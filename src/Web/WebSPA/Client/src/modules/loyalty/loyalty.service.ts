import { Injectable } from "@angular/core";
import { ConfigurationService } from "modules/shared/services/configuration.service";
import { DataService } from "modules/shared/services/data.service";

@Injectable({
  providedIn: "root",
})
export class LoyaltyService {
  private loyaltyUrl: string;

  constructor(
    private dataService: DataService,
    private configurationService: ConfigurationService
  ) {
    this.loyaltyUrl = "";
    this.configurationService.settingsLoaded$.subscribe((x) => {
      this.loyaltyUrl =
        this.configurationService.serverSettings.purchaseUrl +
        "/c/api/v1/catalog/catalogtypes";
    });
  }
}
