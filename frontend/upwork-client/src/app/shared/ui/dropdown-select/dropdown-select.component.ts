import { Component, Input, OnInit } from '@angular/core';
// Value should be taken from input id="dropdown-input-{insertedName}"
@Component({
  selector: 'app-dropdown-select',
  templateUrl: './dropdown-select.component.html',
  styleUrls: ['./dropdown-select.component.css'],
})
export class DropdownSelectComponent implements OnInit {
  buttonPressed(elem: MouseEvent | null, selectName: string | undefined) {
    const selectQuery = "app-dropdown-select[name='" + selectName + "']";
    const value = (elem?.target as HTMLElement).getAttribute('value');
    const display = (elem?.target as HTMLElement).innerText;
    const inputValue = document.querySelector(
      selectQuery + ' .dropdown-input-value'
    ) as HTMLInputElement;
    const inputDisplay = document.querySelector(
      selectQuery + ' .dropdown-input-display'
    ) as HTMLInputElement;
    inputValue.value = value ?? '';
    inputDisplay.value = display;
  }
  constructor() {}
  ngOnInit(): void {
    const selectQuery = "app-dropdown-select[name='" + this.selectName + "']";
    const dropdown = document.querySelector(
      selectQuery + ' div.dropdown'
    ) as HTMLElement;
    dropdown.onclick = function () {
      dropdown.classList.toggle('active');
    };
  }
  // Name of the select, required
  @Input('name') selectName: string | undefined;
  // Default text that is displayed on select when nothing is selected yet, not required
  @Input() defaultText: string | undefined;
  //An array of string that will be displayed on select, if not provided values from actualValues will be displayed, not required
  @Input() displayValues: Array<string> | undefined;
  //An array of strings which represents the value of each option, required
  @Input() actualValues!: Array<string>;
  //Value that you wish to be automatically selected, not required
  @Input() selectedValue: string | undefined;
  //Display value that you wish to be automatically selected, not required
  //if not provided, but selectedValue  and displayValues are provided, program will try to find appropiate value to display from displayValues
  //if it will fail selectedValue itself will be used
  @Input() displayedValue: string | undefined;
}
