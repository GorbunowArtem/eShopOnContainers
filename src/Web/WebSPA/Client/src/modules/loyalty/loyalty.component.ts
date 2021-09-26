import { Component, OnInit } from "@angular/core";
import { LoyaltyService } from "./loyalty.service";

@Component({
  selector: "app-loyalty",
  templateUrl: "./loyalty.component.html",
  styleUrls: ["./loyalty.component.scss"],
})
export class LoyaltyComponent implements OnInit {
  loyaltyPoints: number;

  constructor(private loyaltyService: LoyaltyService) {
    this.loyaltyPoints = 0;
  }

  ngOnInit() {
    this.loyaltyService.getByBuyerId(1).subscribe((lp: number) => {
      this.loyaltyPoints = lp;
    });
  }
}
