import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-loyalty",
  templateUrl: "./loyalty.component.html",
  styleUrls: ["./loyalty.component.scss"],
})
export class LoyaltyComponent implements OnInit {
  loyaltyPoints: number;

  constructor() {
    this.loyaltyPoints = 0;
  }

  ngOnInit() {}
}
