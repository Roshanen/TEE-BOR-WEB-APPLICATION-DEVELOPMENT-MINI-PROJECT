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

function qChangeDisplay() {
  var options = document.querySelectorAll(".dropdown-menu a");

  options.forEach((opt) => {
    opt.addEventListener("click", (ev) => {
      var selectedText = ev.target.innerHTML;
      var selectedId = ev.target.id;
      var dropdownTitle = ev.target.closest("*:has(span)");
      var dropdownName = dropdownTitle.querySelector("span");

      if (selectedId == "any") {
        dropdownName.innerHTML = "Any" + dropdownName.id;
      } else {
        dropdownName.innerHTML = selectedText;
      }

      search();
    });
  });
}

function checkElementAndAny(name) {
  var element = document.getElementById(name);
  var text = element ? element.textContent.trim() : "any";
  text = text.startsWith("Any") ? "any" : text;
  return text;
}

function search() {
  var name = document.getElementById("name").value || "";
  var status = checkElementAndAny("status");
  var dateChoice = checkElementAndAny("day");
  var type = checkElementAndAny("type");
  var category = checkElementAndAny("category");
  var sort = checkElementAndAny("sort");

  var inputValues = {
    Name: name,
    Status: status,
    DateChoice: dateChoice,
    Type: type,
    Category: category,
    Sort: sort,
  };

  if (!window.location.pathname.startsWith("/search")) {
    window.location.href =
      "/search?" + new URLSearchParams(inputValues).toString();
  }

  const newUrl = "/search?" + new URLSearchParams(inputValues).toString();
  history.pushState(null, "", newUrl);

  sendSearch(inputValues);
}

function sendSearch(inputValues) {
  var formData = new FormData();
  for (var key in inputValues) {
    formData.append(key, inputValues[key]);
  }

  var xhttp = new XMLHttpRequest();
  xhttp.onreadystatechange = function () {
    if (this.readyState === 4 && this.status === 200) {
      document.getElementById("searchResultsContainer").innerHTML =
        this.responseText;
    }
  };

  xhttp.onerror = function (error) {
    console.error(error);
  };

  xhttp.open("POST", "http://localhost:5180/search/viewresult", true);
  xhttp.send(formData);
}

document.addEventListener("DOMContentLoaded", () => {
  new Dropdown();

  var searchName = document.getElementById("name");
  searchName.value = sessionStorage.getItem("searchName");
  
  qChangeDisplay();
  search();
});
