
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


const template = () =>{
    
}






const template = document.createElement("template");
template.innerHTML = `
    <h1>
        My Header
    </h1>
    <nav>
        <ul>
            <li>
                <a href="/blah">Blah</a>
            </li>
            <li>
            <a href="/woo">Woo</a>
        </li>
        </ul>
    </nav>
`;

class CustomHeader extends HTMLElement {
    constructor(){
        super();
        const clone = template.content.cloneNode(true);
        this.appendChild(clone);
    }
}

window.customElements.define("custom-header", CustomHeader);




document.addEventListener('DOMContentLoaded', () => new Dropdown);

