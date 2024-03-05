let state = 1;
formSteps = document.querySelectorAll(".step");
backButton = document.querySelectorAll(".back-button");
nextButton = document.querySelectorAll(".next-button");

formSteps.forEach((section, index) => {
  section.style.display = "none";
  if (index == 0) {
    section.style.display = "block";
  }
});

nextButton.forEach((bt) => {
  bt.addEventListener("click", () => {
    state++;
    updateDisplay();
  });
});

backButton.forEach((bt) => {
  bt.addEventListener("click", () => {
    state--;
    updateDisplay();
  });
});

function updateDisplay() {
  console.log("Hey");
  formSteps.forEach((section, index) => {
    section.style.display = "none";
    if (index === state - 1) {
      section.style.display = "block";
    }
  });
}

// console.log(formSteps);
