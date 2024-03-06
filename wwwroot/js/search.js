

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
<div class="date"><h3>Hi</h3></div>
<div class="header"><h2></h2>
<div class="sub-header"><p></p>
<div class="share"><i class="fa-solid fa-upload"></i></div >
<div class="attendee">attendee</div>
</div></div>
</div>`;

class topicElement extends HTMLElement {
    constructor() {
        super();
        const clone = template.content.cloneNode(true);
        this.appendChild(clone);
    }

    static get observedAttributes() {
        return ["name", "avatar", "event-name", "event-date", "event-image", "event-sub-header", "event-attendee"];
    }

    attributeChangedCallback(name, oldValue, newValue) {

        switch(name) {
            case 'event-name':
                this.querySelector(".header h2").innerText = newValue;
                
            case 'event-date':
                this.querySelector(".date h3").innerText = newValue;
            case 'event-image':
                this.querySelector(".pics img").src = newValue;
            case 'event-sub-header':
                this.querySelector(".sub-header p").innerText = newValue;
            case 'event-attendee':
                this.querySelector(".attendee").innerText = newValue + "attendees";
            
        }
    }
}

window.customElements.define("topic-element", topicElement)
document.addEventListener('DOMContentLoaded',  () => new Dropdown);