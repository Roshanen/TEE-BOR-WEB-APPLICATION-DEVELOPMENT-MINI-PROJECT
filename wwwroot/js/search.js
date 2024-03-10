class Dropdown {
  constructor() {
    this.dropdowns = document.querySelectorAll(".dropdown .dropdown-menu");
    if (this.dropdowns.length) {
      this.initialize();
    }
  }

  initialize() {
    document.addEventListener("click", (e) => {
      if (e.target.matches(".dropdown-action, .dropdown-action *")) {
        this.hideOtherDropdown(e.target);
        this.handleClick(e.target);
      } else {
        this.hideOtherDropdown(null);
      }
    });
  }

  handleClick(dropdownAction) {
    this.dropdowns.forEach((dropdown) => {
      if (dropdown.classList.contains("active")) {
        dropdown.classList.remove("active");
      } else if (dropdown.parentElement.contains(dropdownAction)) {
        dropdown.classList.add("active");
      } else {
        dropdown.classList.remove("active");
      }
    });
  }

  hideOtherDropdown(dropdownAction) {
    this.dropdowns.forEach((dropdown) => {
      if (!dropdown.parentElement.contains(dropdownAction)) {
        dropdown.classList.remove("active");
      }
    });
  }
}

document.addEventListener("DOMContentLoaded", () => new Dropdown());

function qChangeDisplay() {
  var options = document.querySelectorAll(".dropdown-menu a");
  options.forEach((opt) => {
    opt.addEventListener("click", (ev) => {
      var selectedText = ev.target.innerHTML;
      var selectedId = ev.target.id;
      var dropdownTitle = ev.target.closest("*:has(span)");
      var dropdownName = dropdownTitle.querySelector("span");

      if (selectedId == "any") {
        dropdownName.innerHTML = "Any " + dropdownName.id;
      } else {
        dropdownName.innerHTML = selectedText;
      }
    });
  });
}

qChangeDisplay();
