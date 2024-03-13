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
    redirectToSearch();
  });
});



function redirectToSearch(){
  var searchNameText = document.getElementById("name").value;
  sessionStorage.setItem('searchName', searchNameText);
  
  if (!window.location.pathname.startsWith("/search")) {
    window.location.href = "/search?";
  }
  else {
    search();
  }
}


