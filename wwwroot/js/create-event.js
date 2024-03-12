let state = 1;
var minProgress = 1;
var maxProgress = 5;
formSteps = document.querySelectorAll(".step");
backButtons = document.querySelectorAll(".back-button");
nextButtons = document.querySelectorAll(".next-button");
circles = document.querySelectorAll(".circle");

function max(a, b) {
  if (a > b) return a;
  else return b;
}

function setButtonToSubmit() {
  let nextButton = document.getElementById("next-button");
  if ((state).toString() >= maxProgress.toString()) {
    nextButton.type = "submit";
  }
  if (state.toString() >= maxProgress.toString()) {
    nextButton.innerText = "Submit";
  } else {
    nextButton.type = "button";
    nextButton.innerText = "Next";
  }
}

function setProgress() {
  circles.forEach((circle, i) => {
    // console.log(circle);
    if (i < state) {
      circle.style.backgroundColor = "#00798A";
      circle.style.color = "#ffffff";
    } else {
      circle.style.backgroundColor = "#ffffff";
      circle.style.color = "#00798A";
    }
  });
}

nextButtons.forEach((bt) => {
  bt.addEventListener("click", () => {
    var stepInputs = formSteps[state - 1].querySelectorAll("input, textarea");
    var validFlag = 1;

    for (let i = 0; i < stepInputs.length; i++) {
      let inp = stepInputs[i];
      if (!inp.checkValidity()) {
        inp.reportValidity();
        validFlag = 0;
        break;
      }
    }

    if (validFlag) {
      // state++;
      state = state < 5 ? ++state : 5;
      updateDisplay();
    }
  });
});

backButtons.forEach((bt) => {
  bt.addEventListener("click", () => {
    state = max(state - 1, minProgress);
    updateDisplay();
  });
});

function updateDisplay() {
  formSteps.forEach((section, index) => {
    section.style.display = "none";
    if (index === state - 1) {
      section.style.display = "block";
    }
  }); // end for each
  setButtonToSubmit();
  setProgress();
}

updateDisplay();
