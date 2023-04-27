import { Component,Input,OnInit } from '@angular/core';

@Component({
  selector: 'app-popup',
  templateUrl: './popup.component.html',
  styleUrls: ['./popup.component.css']
})
export class PopupComponent {

  closePopup(){
    document.querySelector(".popup-content")?.remove();
  }
  
  constructor() {
    this.width="500px";
    this.height="300px";
    this.popupTitle="Popup";
    this.popupInfo="Confirm choice?";
    this.btnPrimaryText="Accept";
    this.btnSecondaryText="Decline";
    this.btnSecondaryFunction=this.closePopup;
  }

  ngOnInit(): void {
    const popup=document.querySelector(".popup-content") as HTMLElement;
    if(this.width){
      popup.style.width=this.width;
    }
    if(this.height){
      popup.style.height=this.height;
    }
    
  
  }
  //Width of the popup with units, default 500px
  @Input("width") width:string;
  //Height of the popup with units, default 300px
  @Input("height") height:string;
  //Title of the popup, default "Popup"
  @Input("popupTitle") popupTitle:string;
  //Content of the popup, default "Confirm choice?"
  @Input("popupInfo") popupInfo:string;
  //Text of the primary button, default "Accept"
  @Input("btnPrimaryText") btnPrimaryText:string;
  //Text of the secondary button, default "Decline"
  @Input("btnSecondaryText") btnSecondaryText: string;
  //Function that will be executed on clicking primary button, takes no argument and returns none
  @Input("btnPrimaryFunction") btnPrimaryFunction: (() => void | undefined) | undefined;
  //Function that will be executed on clicking secondary button, takes no argument and returns none, default remove popup
  @Input("btnSecondaryFunction") btnSecondaryFunction: () => void;
}
