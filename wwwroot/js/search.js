class Dropdown {
    constructor() {
        this.dropdowns = document.querySelectorAll('.dropdown .dropdown-menu');
        if (this.dropdowns.length) { //check if the dropdown exist
            this.initialize();
        }
    }

    initialize() {
        document.addEventListener('click', (e) => {
            if (e.target.classList.contains('dropdown-action')) { //Listen to the clicked element
            this.hideOtherDropdown(e.target);
            this.handleClick(e.target);
        } else {
            this.hideOtherDropdown(null);
        }
        });
    }

    handleClick(dropdownAction){
        this.dropdowns.forEach(dropdown => {
            if (dropdown.parentElement.contains(dropdownAction)){
                dropdown.classList.add("active");
            }
            else{
                dropdown.classList.remove("active");
            }
        });
    }

    hideOtherDropdown(dropdownAction){
        this.dropdowns.forEach((dropdown) => {
            if (!dropdown.parentElement.contains(dropdownAction)){
                dropdown.classList.remove('active');
            }
        });
    }
}

const template = document.createElement("template");
template.innerHTML = `
<link rel="stylesheet" href="../css/card.css">

<div class="block">
<div class="pics">
    <img class="img" src="https://i.ibb.co/6Z6MftT/chigiri.jpg"/>
</div>
<div class="details">
<div class="date"><h3>THU, FEB 29 · 6:00 PM ICT</h3></div>
<div class="header"><h2> Goodbye VPN! Hello Microsoft Global Secure Access</h2>
<div class="sub-header"><p>.NET Developers Community Singapore</p>
<div class="share"><i class="fa-solid fa-upload"></i></div >
<div class="attendee"> 1 attendee</div>
</div></div>
</div>`;

class topicElement extends HTMLElement {
    constructor() {
        super();

        const clone = template.content.cloneNode(true);
        this.appendChild(clone);
        console.log("hello world");
    }

    static get observedAttributes() {
        return ["name", "avatar"];
    }

    attributeChangedCallback(name, oldValue, newValue) {
        this.shadowRoot.querySelector(".details h2").innerText =
        this.getAttribute("name");
        this.shadowRoot.querySelector(".avatar img").src =
        this.getAttribute("avatar");
        this.shadowRoot.querySelector(".avatar img").alt =
        this.getAttribute("name");
    }
}


window.customElements.define("topic-element", topicElement)
document.addEventListener('DOMContentLoaded',  () => new Dropdown);