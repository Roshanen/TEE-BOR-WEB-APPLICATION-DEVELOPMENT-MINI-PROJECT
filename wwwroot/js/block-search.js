const baseBlock = document.querySelector(".search-base");
const extendBlock = document.querySelector(".search-extend");

const inputBaseBlock = baseBlock.childNodes[1].childNodes[3];

inputBaseBlock.addEventListener("focus", () => {
  extendBlock.style.display = "flex";
});

function search() {
  var name = $("#name").val() || "";
  var place = $("#place").val() || "";
  var dateChoice = $("#dateChoice").val() || "any";
  var type = $("#type").val() || "any";
  var category = $("#category").val() || "any";

  var inputValues = {
      name: name,
      place: place,
      dateChoice: dateChoice,
      type: type,
      category: category
  };

  $.ajax({
      type: "POST",
      url: "/search",
      data: inputValues,
      success: function (data) {
          $("#searchResultsContainer").html(data);
          $("#name").val(inputValues.name);
          $("#place").val(inputValues.place);
          $("#dateChoice").val(inputValues.dateChoice);
          $("#type").val(inputValues.type);
          $("#category").val(inputValues.category);
      },
      error: function (error) {
          console.error(error);
      }
  });
}
