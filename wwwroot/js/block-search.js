const baseBlock = document.querySelector(".search-base");
const extendBlock = document.querySelector(".search-extend");

const inputBaseBlock = baseBlock.childNodes[1].childNodes[3];

inputBaseBlock.addEventListener("focus", () => {
  extendBlock.style.display = "flex";
});

extendBlock.style.display = "flex";

const bSubmits = document.querySelectorAll(
  ".submit-button, .block-submit-button"
);

bSubmits.forEach((b) => {
  b.addEventListener("click", () => {
    redirectToSearch();
  });
});

const blockInput = document.querySelector(".block-search-name #name");
const navInput = document.querySelector(".search-name #name");

navInput.addEventListener("change", () => {
  blockInput.value = navInput.value;
})

blockInput.addEventListener("change", () => {
  navInput.value = blockInput.value;
})

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


