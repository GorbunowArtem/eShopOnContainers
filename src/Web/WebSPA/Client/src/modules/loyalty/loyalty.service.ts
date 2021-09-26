import {Injectable} from "@angular/core";
import {ConfigurationService} from "modules/shared/services/configuration.service";
import {DataService} from "modules/shared/services/data.service";
import {Observable} from "rxjs";
import {IOrder} from "../shared/models/order.model";
import {tap} from "rxjs/operators";

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
                "/l/api/v1/";
        });
    }

    public getByBuyerId(id: number): Observable<number> {
        let url = `${this.loyaltyUrl}loyalty-points/${id}`;

        return this.dataService.get(url).pipe<number>(tap((response: any) => {
            return response;
        }));
    }
}
