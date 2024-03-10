const baseBlock = document.querySelector(".search-base");
const extendBlock = document.querySelector(".search-extend");

const inputBaseBlock = baseBlock.childNodes[1].childNodes[3];

inputBaseBlock.addEventListener("focus", () => {
  extendBlock.style.display = "flex";
});

const bSubmits = document.querySelectorAll(
  ".submit-button, .block-submit-button"
);

bSubmits.forEach((b) =>
  b.addEventListener("click", () => {
    search();
    console.log("se");
  })
);

function search() {
  var name = $("#name").val() || "";
  var place = $("#place").val() || "";
  var dateChoice = $("#dateChoice").text() || "any";
  var type = $("#type").text() || "any";
  var category = $("#category").text() || "any";

  var inputValues = {
    name: name,
    place: place,
    dateChoice: dateChoice,
    type: type,
    category: category,
  };

  $.ajax({
    type: "POST",
    url: "http://localhost:5180//search",
    data: inputValues,
    success: function (data) {
      $("#searchResultsContainer").html(data);
      // $("#name").val(inputValues.name);
      // $("#place").val(inputValues.place);
      // $("#dateChoice").text(inputValues.dateChoice);
      // $("#type").text(inputValues.type);
      // $("#category").text(inputValues.category);
    },
    error: function (error) {
      console.error(error);
    },
  });
}
