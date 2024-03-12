const baseBlock = document.querySelector(".search-base");
const extendBlock = document.querySelector(".search-extend");

const inputBaseBlock = baseBlock.childNodes[1].childNodes[3];

inputBaseBlock.addEventListener("focus", () => {
  extendBlock.style.display = "flex";
});

const bSubmits = document.querySelectorAll(
  ".submit-button, .block-submit-button"
);

bSubmits.forEach((b) => {
  b.addEventListener("click", () => {
    search();
  });
});

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
    window.location.href = "/search?name=" + name;
  } else {
    sendSearch(inputValues);
  }
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

  // fetch("http://localhost:5180/search/viewresult", {
  //   method: "POST",
  //   body: formData,
  // })
  //   .then(function (response) {
  //     return response.text();
  //   })
  //   .then(function (data) {
  //     document.getElementById("searchResultsContainer").innerHTML = data;
  //   })
  //   .catch(function (error) {
  //     console.error(error);
  //   });
}
