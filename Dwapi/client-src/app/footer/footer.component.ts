import { Component, OnInit } from '@angular/core';
import { AppDetailsService } from '../services/app-details.service';
import { Message } from 'primeng/api';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'liveapp-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  private _appDetailsService: AppDetailsService;
  public errorMessage: Message[];
  public loadVersion$: Subscription;
  public version: string;

  constructor(appDetailsService: AppDetailsService) {
    this._appDetailsService = appDetailsService;
  }

  ngOnInit() {
    this.getVersion();
  }

  public getVersion(): void {
    this.errorMessage = [];
    this.loadVersion$ = this._appDetailsService.getVersion()
        .subscribe(
            p => {
              this.version = p;
            },
            e => {
                this.errorMessage = [];
                this.errorMessage.push({
                    severity: 'error',
                    summary: 'Error loading app version',
                    detail: <any>e
                });
            },
            () => {
            }
        );
}

}
