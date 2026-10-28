<script>
export default {
  data() {
    return {
      expression: '',
      showFunctions: false,
      functions: [],
      name: '',
      formula: '',
      argumentsInput: '',
      error: '',
      keys: [
        'AC', '(', ')', '/',
        '7', '8', '9', '*',
        '4', '5', '6', '-',
        '1', '2', '3', '+',
        '⌫', '0', ',', '=',
      ],
    }
  },

  methods: {
    press(key) {
      if (key === 'AC') {
        this.expression = ''
      } else if (key === '⌫') {
        this.expression = this.expression.slice(0, -1)
      } else if (key === '=') {
        this.prepareExpression()
      } else {
        this.expression += key === ',' ? '.' : key
      }
    },

    async prepareExpression() {
      if (!this.expression.trim()) return

    

      const res = await fetch("https://localhost:5001/api/create_function", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(this.expression),
      })
    },

    async createFunction() {
      const name = this.name.trim()
      const formula = this.formula.trim()
      const identifier = /^[a-zA-Z_][a-zA-Z0-9_]*$/

      const parameters = this.argumentsInput.trim()
        ? this.argumentsInput.split(',').map(arg => arg.trim())
        : []

      if (!identifier.test(name)) {
        this.error = 'Entre un nom de fonction valide, sans espace.'
        return
      }

      if (parameters.some(arg => !identifier.test(arg))) {
        this.error = 'Chaque argument doit avoir un nom valide, par exemple : x, y.'
        return
      }

      if (new Set(parameters).size !== parameters.length) {
        this.error = 'Les arguments doivent avoir des noms différents.'
        return
      }

      if (!formula) {
        this.error = 'Entre une expression.'
        return
      }

      if (this.functions.some(fn => fn.name === name)) {
        this.error = 'Ce nom de fonction existe déjà.'
        return
      }

      const res = await fetch("https://localhost:5000/api/create_function", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name, parameters, formula }),
      })

      this.name = ''
      this.argumentsInput = ''
      this.formula = ''
      this.error = ''
      this.showFunctions = false
    },
  },
}
</script>

<template>
  <main class="calc-app">
    <section class="calc-card">
      <header class="calc-header">
        <div>
          <span class="calc-caption">TON ESPACE DE CALCUL</span>
          <h1>Calculatrice<span>.</span></h1>
        </div>

        <button class="calc-function-toggle" type="button" @click="showFunctions = !showFunctions">
          {{ showFunctions ? '← Retour' : 'ƒ Nouvelle fonction' }}
        </button>
      </header>

      <!-- Interface de création de fonction -->
      <form v-if="showFunctions" class="calc-form" @submit.prevent="createFunction">
        <h2>Créer une fonction</h2>
        <p>Choisis un nom, ses arguments et son expression.</p>

        <label for="fn-name">Nom de la fonction</label>
        <input id="fn-name" v-model="name" placeholder="addition" autocomplete="off" />

        <label for="fn-arguments">Arguments</label>
        <input id="fn-arguments" v-model="argumentsInput" placeholder="a, b" autocomplete="off"
          aria-describedby="fn-arguments-help" />
        <small id="fn-arguments-help" class="calc-input-help">
          Sépare les arguments par des virgules. Exemple : x, y, z.
          Laisse vide pour une fonction sans argument.
        </small>

        <label for="fn-expression">Expression</label>
        <input id="fn-expression" v-model="formula" placeholder="a + b" autocomplete="off" />

        <div class="calc-preview">
          {{ name || 'fonction' }}({{
            argumentsInput
              .split(',')
              .map(arg => arg.trim())
              .filter(Boolean)
              .join(', ')
          }}) = {{ formula || '…' }}
        </div>

        <p v-if="error" class="calc-error" role="alert">
          {{ error }}
        </p>

        <button class="calc-save" type="submit">
          Enregistrer la fonction
        </button>
      </form>

      <!-- Interface calculatrice -->
      <template v-else>
        <div class="calc-display" aria-live="polite">
          <span class="calc-caption">EXPRESSION</span>
          <input v-model="expression" class="calc-display" type="text" placeholder="addition(2, 3)"
            aria-label="Expression à calculer" autocomplete="off" :spellcheck="false"
            @keydown.enter.prevent="prepareExpression" />
        </div>

        <div v-if="functions.length" class="calc-functions">
          <button v-for="fn in functions" :key="fn.name" type="button" :title="fn.expression" @click="
            expression += fn.name + (fn.parameters.length ? '(' : '()')
            ">
            {{ fn.name }}({{ fn.parameters.join(', ') }}) ↗
          </button>
        </div>
        <div class="calc-functions">
          <button type="button" aria-label="Insérer une virgule pour séparer les arguments" @click="expression += ', '">
            , Séparer les arguments
          </button>
        </div>

        <div class="calc-keypad">
          <button v-for="key in keys" :key="key" type="button" :class="{
            'calc-operator': ['/', '*', '-', '+'].includes(key),
            'calc-equal': key === '=',
            'calc-clear': key === 'AC',
          }" :aria-label="key === '⌫'
                ? 'Supprimer le dernier caractère'
                : key === 'AC'
                  ? 'Tout effacer'
                  : key
              " @click="press(key)">
            {{ key === '*' ? '×' : key === '/' ? '÷' : key }}
          </button>
        </div>

      </template>
    </section>
  </main>
</template>
<style lang="scss">

@import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&display=swap');

:root {
  color-scheme: dark;
  font-family: 'Inter', sans-serif;
  background: #0b0e14;
  color: #edf1f7;
}

html,
body,
#app {
  margin: 0;
  min-height: 100%;
  width: 100%;
}

.calc-app,
.calc-app * {
  box-sizing: border-box;
}

body {
  margin: 0;
}

.calc-app {
  --accent: #c1ef92;
  --muted: #8792a5;
  min-height: 100vh;
  min-height: 100dvh;
  display: grid;
  place-items: center;
  padding: 40px 20px;
  background:
    radial-gradient(ellipse at 20% 15%, #b5ef9212, transparent 45%),
    radial-gradient(ellipse at 85% 85%, #839cff12, transparent 45%),
    #0b0e14;
  color: #edf1f7;
  font-family: 'Inter', sans-serif;
}

.calc-app button,
.calc-app input {
  font: inherit;
  -webkit-tap-highlight-color: transparent;
}

.calc-app button {
  cursor: pointer;
  touch-action: manipulation;
}

.calc-app button:focus-visible,
.calc-app input:focus-visible {
  outline: 2px solid var(--accent);
  outline-offset: 4px;
}

.calc-card {
  position: relative;
  width: 100%;
  max-width: 490px;
  padding: 30px;
  border: 1px solid #ffffff12;
  border-radius: 30px;
  background: linear-gradient(145deg, #202630ed, #141922f5);
  box-shadow:
    0 30px 90px #00000060,
    inset 0 1px 0 #ffffff09;
  backdrop-filter: blur(24px);
  animation: card-appear 400ms ease-out both;
}

.calc-card::before {
  position: absolute;
  top: -1px;
  left: 20%;
  right: 20%;
  height: 1px;
  background: linear-gradient(90deg, transparent, #c1ef9270, transparent);
  content: '';
}

.calc-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 14px;
  margin-bottom: 28px;
}

.calc-caption {
  color: var(--muted);
  font-size: 9px;
  font-weight: 700;
  letter-spacing: 1.7px;
}

.calc-header h1 {
  margin: 8px 0 0;
  font-size: 27px;
  font-weight: 800;
  letter-spacing: -1.3px;
}

.calc-header h1 span {
  color: var(--accent);
}

.calc-function-toggle {
  flex-shrink: 0;
  padding: 11px 13px;
  border: 1px solid #c1ef9225;
  border-radius: 12px;
  background: #c1ef920a;
  color: var(--accent);
  font-size: 11px !important;
  font-weight: 600 !important;
  transition:
    background 180ms,
    border-color 180ms,
    transform 180ms;
}

.calc-function-toggle:hover {
  transform: translateY(-2px);
  border-color: #c1ef9250;
  background: #c1ef9215;
}

/* Conteneur de l'écran */
div.calc-display {
  margin-bottom: 22px;
  padding: 18px 20px 12px;
  overflow: hidden;
  border: 1px solid #ffffff0b;
  border-radius: 20px;
  background: #0b0f17;
  box-shadow: inset 0 2px 10px #00000025;
  transition: border-color 180ms, box-shadow 180ms;
}

div.calc-display:focus-within {
  border-color: #c1ef9245;
  box-shadow: 0 0 0 4px #c1ef9206;
}

div.calc-display > .calc-caption {
  display: flex;
  align-items: center;
  gap: 7px;
}

div.calc-display > .calc-caption::before {
  width: 5px;
  height: 5px;
  border-radius: 50%;
  background: var(--accent);
  box-shadow: 0 0 10px #c1ef9250;
  content: '';
}

/* Champ éditable : pas de deuxième cadre dans l'écran */
.calc-app input.calc-display {
  display: block;
  width: 100%;
  min-width: 0;
  height: 85px;
  margin: 0;
  padding: 14px 0 4px;
  border: 0;
  border-radius: 0;
  outline: none;
  background: transparent;
  box-shadow: none;
  color: #f2f6fc;
  font-size: 30px;
  font-weight: 500;
  font-variant-numeric: tabular-nums;
  letter-spacing: -1px;
  text-align: right;
  caret-color: var(--accent);
}

.calc-app input.calc-display::placeholder {
  color: #414c5e;
  font-size: 23px;
  letter-spacing: -0.5px;
}

.calc-functions {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
  margin-bottom: 14px;
}

.calc-functions button {
  max-width: 100%;
  padding: 9px 12px;
  overflow-wrap: anywhere;
  border: 1px solid #c1ef921a;
  border-radius: 10px;
  background: #c1ef9208;
  color: #c5ddaF;
  font-size: 11px;
  font-weight: 500;
  transition: background 180ms, border-color 180ms;
}

.calc-functions button:hover {
  border-color: #c1ef9240;
  background: #c1ef9215;
}

.calc-keypad {
  display: grid;
  grid-template-columns: repeat(4, minmax(0, 1fr));
  gap: 10px;
  margin-top: 20px;
}

.calc-keypad button {
  display: grid;
  place-items: center;
  height: 66px;
  padding: 0;
  border: 1px solid #ffffff08;
  border-radius: 16px;
  background: linear-gradient(145deg, #2a313d, #232a35);
  box-shadow:
    0 3px 5px #00000015,
    inset 0 1px 0 #ffffff04;
  color: #e6ebf4;
  font-size: 22px;
  font-weight: 500;
  transition:
    background 150ms,
    border-color 150ms,
    box-shadow 150ms,
    transform 150ms;
}

.calc-keypad button:hover {
  transform: translateY(-2px);
  border-color: #ffffff18;
  background: #333d4c;
  box-shadow: 0 6px 12px #00000025;
}

.calc-keypad button:active {
  transform: translateY(1px) scale(0.96);
  box-shadow: none;
}

.calc-keypad .calc-operator {
  border-color: #c1ef9212;
  background: #c1ef920c;
  color: var(--accent);
  font-size: 25px;
}

.calc-keypad .calc-operator:hover {
  border-color: #c1ef9235;
  background: #c1ef921c;
}

.calc-keypad .calc-clear {
  background: #ffac9b0b;
  color: #ffb4a5;
  font-size: 16px;
  font-weight: 700;
}

.calc-keypad .calc-clear:hover {
  border-color: #ffac9b30;
  background: #ffac9b18;
}

.calc-keypad .calc-equal {
  border-color: #d4ffac50;
  background: linear-gradient(135deg, #d0f8a8, #a8df75);
  box-shadow: 0 4px 18px #b5ed8b18;
  color: #233519;
  font-size: 29px;
}

.calc-keypad .calc-equal:hover {
  border-color: #e1ffc1;
  background: linear-gradient(135deg, #dfffc0, #b9ed8c);
  box-shadow: 0 6px 24px #b5ed8b30;
}

/* Création des fonctions */
.calc-form {
  animation: card-appear 250ms ease-out both;
}

.calc-form h2 {
  margin: 0 0 8px;
  font-size: 23px;
  font-weight: 700;
  letter-spacing: -0.8px;
}

.calc-form > p {
  margin: 0 0 24px;
  color: var(--muted);
  font-size: 12px;
  line-height: 1.7;
}

.calc-form label {
  display: block;
  margin: 20px 0 9px;
  color: #cbd3df;
  font-size: 12px;
  font-weight: 600;
}

.calc-form input {
  width: 100%;
  padding: 14px 15px;
  border: 1px solid #ffffff10;
  border-radius: 12px;
  outline: none;
  background: #0e131c;
  color: #edf1f7;
  font-size: 14px;
  transition: border-color 180ms, box-shadow 180ms;
}

.calc-form input::placeholder {
  color: #4f5b6e;
}

.calc-form input:focus {
  border-color: #c1ef9260;
  box-shadow: 0 0 0 4px #c1ef9208;
}

.calc-input-help {
  display: block;
  margin-top: 9px;
  color: var(--muted);
  font-size: 10px;
  line-height: 1.8;
}

.calc-preview {
  margin: 24px 0;
  padding: 18px;
  overflow-wrap: anywhere;
  border: 1px dashed #c1ef9230;
  border-radius: 13px;
  background: #c1ef9206;
  color: var(--accent);
  font-family: ui-monospace, SFMono-Regular, Consolas, monospace;
  font-size: 14px;
  line-height: 1.6;
}

.calc-save {
  width: 100%;
  padding: 16px;
  border: 1px solid #d4ffac40;
  border-radius: 13px;
  background: linear-gradient(135deg, #d0f8a8, #a8df75);
  box-shadow: 0 5px 20px #b5ed8b12;
  color: #233519;
  font-size: 13px !important;
  font-weight: 700 !important;
  transition: transform 180ms, box-shadow 180ms;
}

.calc-save:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 25px #b5ed8b25;
}

.calc-save:active {
  transform: scale(0.98);
}

.calc-form .calc-error {
  margin-bottom: 18px;
  padding: 12px 14px;
  border: 1px solid #ffac9b20;
  border-radius: 10px;
  background: #ffac9b08;
  color: #ffb4a5;
  font-size: 12px;
}

::selection {
  background: #c1ef9240;
  color: #fff;
}

@keyframes card-appear {
  from {
    opacity: 0;
    transform: translateY(12px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@media (max-width: 420px) {
  .calc-app {
    padding: 20px 12px;
  }

  .calc-card {
    padding: 22px 18px;
    border-radius: 24px;
  }

  .calc-header {
    align-items: flex-start;
    flex-direction: column;
    gap: 16px;
    margin-bottom: 22px;
  }

  .calc-function-toggle {
    width: 100%;
  }

  .calc-keypad {
    gap: 8px;
  }

  .calc-keypad button {
    height: 58px;
    border-radius: 13px;
  }

  .calc-app input.calc-display {
    font-size: 26px;
  }
}

@media (prefers-reduced-motion: reduce) {
  .calc-app *,
  .calc-app *::before {
    animation: none !important;
    transition: none !important;
  }
}
</style>