<script setup>
const buttons = [
  { label: 'sin', style: 'function' },
  { label: 'cos', style: 'function' },
  { label: 'tan', style: 'function' },
  { label: '√', style: 'function' },
  { label: 'AC', style: 'clear' },

  { label: 'ln', style: 'function' },
  { label: 'log', style: 'function' },
  { label: 'x²', style: 'function' },
  { label: 'xʸ', style: 'function' },
  { label: '÷', style: 'operator' },

  { label: '(', style: 'function' },
  { label: ')', style: 'function' },
  { label: 'π', style: 'function' },
  { label: 'e', style: 'function' },
  { label: '×', style: 'operator' },

  { label: '7', style: 'number' },
  { label: '8', style: 'number' },
  { label: '9', style: 'number' },
  { label: 'DEL', style: 'function' },
  { label: '−', style: 'operator' },

  { label: '4', style: 'number' },
  { label: '5', style: 'number' },
  { label: '6', style: 'number' },
  { label: '1/x', style: 'function' },
  { label: '+', style: 'operator' },

  { label: '1', style: 'number' },
  { label: '2', style: 'number' },
  { label: '3', style: 'number' },
  { label: '±', style: 'function' },
  { label: '=', style: 'equals' },

  { label: '0', style: 'number zero' },
  { label: '.', style: 'number' },
  { label: '%', style: 'function' },
]
  
</script>

<template>
  <main class="page">
    <header class="topbar">
      <a class="brand" href="#">
        <span class="brand-icon">∑</span>
        <span>nova<span class="brand-light">calc</span></span>
      </a>

      <span class="status"><i></i> CALCULATRICE SCIENTIFIQUE</span>
    </header>

    <section class="content">
      <div class="intro">
        <span class="eyebrow">VOTRE ESPACE DE CALCUL</span>
        <h1>Les maths,<br /><span>en toute simplicité.</span></h1>
        <p>
          Une interface claire et moderne pour vos calculs du quotidien
          et vos opérations scientifiques.
        </p>

        <div class="feature">
          <span class="feature-icon">⌁</span>
          <div>
            <strong>Fonctions scientifiques</strong>
            <small>Trigonométrie, puissances et racines</small>
          </div>
        </div>

        <div class="feature">
          <span class="feature-icon">◷</span>
          <div>
            <strong>Historique des calculs</strong>
            <small>Retrouvez facilement vos opérations</small>
          </div>
        </div>

        <div class="keyboard-tip">
          <span>⌨</span> Compatible avec votre clavier
        </div>
      </div>

      <section class="calculator">
        <header class="calc-header">
          <div class="calc-title">
            <span class="calc-icon">ƒx</span>
            <div>
              <h2>Calculatrice scientifique</h2>
              <p>Mode avancé</p>
            </div>
          </div>

          <span class="mode">DEG</span>
        </header>

        <div class="display">
          <div class="expression">sin(30) + cos(60)</div>
          <div class="answer">1<span class="cursor"></span></div>
          <span class="display-caption">RÉSULTAT</span>
        </div>

        <div class="keypad">
          <button
            v-for="(button, index) in buttons"
            :key="`${button.label}-${index}`"
            type="button"
            :class="['key', button.style]"
          >
            {{ button.label }}
          </button>
        </div>

        <footer class="calc-footer">
          <span>Précision optimale</span>
          <span><i></i> Prêt à calculer</span>
        </footer>
      </section>
    </section>

    <footer class="page-footer">
      <span>© 2025 NOVACALC</span>
      <span>Une interface pensée pour vous.</span>
    </footer>
  </main>
</template>

<style>
@import url('https://fonts.googleapis.com/css2?family=DM+Sans:wght@400;500;600;700&family=Manrope:wght@400;500;600;700;800&display=swap');

:root {
  font-family: 'DM Sans', sans-serif;
  color: #f5f5fa;
  background: #101014;
  font-synthesis: none;
  text-rendering: optimizeLegibility;
}

* {
  box-sizing: border-box;
}

body {
  min-width: 320px;
  min-height: 100vh;
  margin: 0;
  background:
    radial-gradient(ellipse at 75% 45%, #30221f 0, transparent 36%),
    radial-gradient(ellipse at 12% 85%, #1b1c29 0, transparent 38%),
    #101014;
}

button {
  font: inherit;
}

.page {
  width: min(1180px, calc(100% - 48px));
  min-height: 100vh;
  margin: auto;
  display: flex;
  flex-direction: column;
}

.topbar {
  height: 86px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  border-bottom: 1px solid #ffffff12;
}

.brand {
  display: flex;
  align-items: center;
  gap: 11px;
  color: #f6f2ef;
  font-family: 'Manrope', sans-serif;
  font-size: 21px;
  font-weight: 800;
  text-decoration: none;
  letter-spacing: -1px;
}

.brand-icon {
  width: 35px;
  height: 35px;
  display: grid;
  place-items: center;
  border-radius: 11px;
  color: #171411;
  background: #e8a17b;
  font-size: 22px;
}

.brand-light {
  color: #e8a17b;
}

.status {
  color: #9b999d;
  font-size: 10px;
  letter-spacing: 1.5px;
}

.status i,
.calc-footer i {
  display: inline-block;
  width: 7px;
  height: 7px;
  margin-right: 8px;
  border-radius: 50%;
  background: #79d7a0;
  box-shadow: 0 0 12px #79d7a080;
}

.content {
  flex: 1;
  display: grid;
  grid-template-columns: 1fr 440px;
  align-items: center;
  gap: 90px;
  padding: 55px 0;
}

.intro {
  max-width: 490px;
}

.eyebrow {
  color: #e8a17b;
  font-size: 11px;
  font-weight: 700;
  letter-spacing: 2px;
}

.intro h1 {
  margin: 22px 0 18px;
  font-family: 'Manrope', sans-serif;
  font-size: clamp(38px, 5vw, 59px);
  line-height: 1.12;
  letter-spacing: -3px;
}

.intro h1 span {
  color: #d39a7b;
}

.intro > p {
  max-width: 415px;
  margin: 0;
  color: #a5a2a7;
  font-size: 15px;
  line-height: 1.8;
}

.feature {
  display: flex;
  align-items: center;
  gap: 15px;
  margin-top: 30px;
}

.feature-icon {
  width: 42px;
  height: 42px;
  display: grid;
  place-items: center;
  border: 1px solid #ffffff14;
  border-radius: 13px;
  color: #e8a17b;
  background: #ffffff08;
  font-size: 22px;
}

.feature strong,
.feature small {
  display: block;
}

.feature strong {
  margin-bottom: 5px;
  font-size: 13px;
}

.feature small {
  color: #96939a;
  font-size: 12px;
}

.keyboard-tip {
  display: inline-flex;
  align-items: center;
  gap: 9px;
  margin-top: 42px;
  padding: 10px 13px;
  border: 1px solid #ffffff12;
  border-radius: 10px;
  color: #aaa7ad;
  font-size: 11px;
}

.keyboard-tip span {
  color: #e8a17b;
  font-size: 17px;
}

.calculator {
  padding: 24px;
  border: 1px solid #ffffff19;
  border-radius: 23px;
  background: linear-gradient(145deg, #222126f5, #19181cf5);
  box-shadow: 0 30px 90px #0008, inset 0 1px #ffffff0b;
}

.calc-header,
.calc-title {
  display: flex;
  align-items: center;
}

.calc-header {
  justify-content: space-between;
}

.calc-title {
  gap: 12px;
}

.calc-icon {
  width: 40px;
  height: 40px;
  display: grid;
  place-items: center;
  border-radius: 12px;
  color: #e8a17b;
  background: #e8a17b18;
  font-size: 18px;
  font-weight: 700;
}

.calc-title h2 {
  margin: 0 0 4px;
  font-size: 13px;
  font-weight: 600;
}

.calc-title p {
  margin: 0;
  color: #929097;
  font-size: 11px;
}

.mode {
  padding: 7px 10px;
  border: 1px solid #ffffff18;
  border-radius: 8px;
  color: #e8a17b;
  font-size: 10px;
  font-weight: 700;
}

.display {
  position: relative;
  min-height: 135px;
  margin: 22px 0 17px;
  padding: 21px;
  overflow: hidden;
  border: 1px solid #ffffff0d;
  border-radius: 15px;
  background: #111115;
  text-align: right;
}

.expression {
  min-height: 24px;
  color: #99969e;
  font-size: 13px;
}

.answer {
  margin-top: 9px;
  font-family: 'Manrope', sans-serif;
  font-size: 39px;
  font-weight: 600;
  letter-spacing: -1px;
}

.cursor {
  display: inline-block;
  width: 2px;
  height: 31px;
  margin-left: 5px;
  background: #e8a17b;
  vertical-align: -4px;
}

.display-caption {
  position: absolute;
  right: 20px;
  bottom: 12px;
  color: #65636a;
  font-size: 8px;
  letter-spacing: 1.6px;
}

.keypad {
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  gap: 9px;
}

.key {
  min-height: 48px;
  border: 1px solid #ffffff0c;
  border-radius: 11px;
  color: #efedf0;
  background: #2a292f;
  cursor: default;
  font-size: 14px;
  font-weight: 500;
  transition: background 0.2s, transform 0.2s;
}

.key:hover {
  background: #37353c;
  transform: translateY(-1px);
}

.key.function {
  color: #d9b29e;
  background: #302d30;
  font-size: 12px;
}

.key.operator {
  color: #e8a17b;
  background: #302a29;
  font-size: 19px;
}

.key.clear {
  color: #ff9d91;
  background: #422a2a;
  font-size: 12px;
  font-weight: 700;
}

.key.equals {
  border-color: #e8a17b;
  color: #211813;
  background: #e8a17b;
  font-size: 20px;
  font-weight: 700;
}

.key.zero {
  grid-column: span 2;
}

.calc-footer {
  display: flex;
  justify-content: space-between;
  margin-top: 17px;
  color: #85828a;
  font-size: 10px;
}

.calc-footer i {
  width: 6px;
  height: 6px;
  margin-right: 4px;
}

.page-footer {
  display: flex;
  justify-content: space-between;
  padding: 20px 0;
  border-top: 1px solid #ffffff12;
  color: #77747c;
  font-size: 10px;
}

@media (max-width: 850px) {
  .content {
    grid-template-columns: 1fr 400px;
    gap: 35px;
  }
}

@media (max-width: 700px) {
  .page {
    width: min(440px, calc(100% - 32px));
  }

  .content {
    grid-template-columns: 1fr;
    gap: 38px;
    padding: 42px 0;
  }

  .intro {
    text-align: center;
  }

  .intro > p {
    margin: auto;
  }

  .feature {
    max-width: 300px;
    margin-right: auto;
    margin-left: auto;
    text-align: left;
  }

  .keyboard-tip {
    margin-top: 28px;
  }

  .calculator {
    padding: 18px;
  }
}

@media (max-width: 380px) {
  .status {
    display: none;
  }

  .keypad {
    gap: 6px;
  }

  .key {
    min-height: 44px;
  }

  .calculator {
    padding: 14px;
  }
}
</style>