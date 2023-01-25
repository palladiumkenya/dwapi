import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreadcrumbService} from '../app/breadcrumb.service';
import {EmrSystem} from '../settings/model/emr-system';
import {AutoloadService} from '../settings/services/autoload.service';
import {ConfirmationService, Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {ProgressBarModule} from 'primeng/progressbar';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {HttpClient} from "@angular/common/http";
import {ProtocolConfigService} from '../settings/services/protocol-config.service';
import {DatabaseProtocol} from "../settings/model/database-protocol";
import {environment} from "../environments/environment";
import {EmrConfigService} from "../settings/services/emr-config.service";
import {EmrMetrics} from "../settings/model/emr-metrics";
import {NdwhDocketComponent} from '../dockets/ndwh-docket/ndwh-docket.component';



@Component({
    selector: 'liveapp-autoload',
    templateUrl: './autoload.component.html',
    styleUrls: ['./autoload.component.scss']
})
export class AutoloadComponent implements OnInit, OnDestroy {

    private _confirmationService: ConfirmationService;
    private _autoloadService: AutoloadService;
    private _emrConfigService: EmrConfigService;


    public getEmr$: Subscription;
    public getMetrics$: Subscription;
    public getRefresh$: Subscription;
    public getMiddleware$: Subscription;
    public emrSystem: EmrSystem;
    public middlewareSystem: EmrSystem;
    public errorMessage: Message[];
    public otherMessage: Message[];
    public loadingData: boolean;
    public canSend: boolean;
    public canSendHts: boolean;
    public canSendMnch: boolean;
    public canSendPrep: boolean;

    public ctSendingComplete: boolean;
    public htsSendingComplete: boolean;
    public mnchSendingComplete: boolean;
    public prepSendingComplete: boolean;

    public metricMessages: Message[];
    public emrVersion: string;
    public emrMetric: EmrMetrics;

    // private _url: string = './api/RefreshETL';
    private _http: HttpClient;
    public dbProtocol: DatabaseProtocol;

    step: number = 1;
    barValue: number = 1;
    stepOneIsActive: string = "form-stepper-active step-section-active";
    stepTwoIsActive: string = "form-stepper-unfinished step-section-inactive";
    stepThreeIsActive: string = "form-stepper-unfinished step-section-inactive";
    stepFourIsActive: string = "form-stepper-unfinished step-section-inactive";
    stepFiveIsActive: string = "form-stepper-unfinished step-section-inactive";



    public constructor(public breadcrumbService: BreadcrumbService, emrConfigService: EmrConfigService,
                       // ndwhConsoleComponent: NdwhConsoleComponent,
                        confirmationService: ConfirmationService, autoloadService: AutoloadService, http: HttpClient)
    {
        this.breadcrumbService.setItems([
            {label: 'autoload'}
        ]);
        this._emrConfigService = emrConfigService;
        this._confirmationService = confirmationService;
        this._autoloadService = autoloadService;
        this._http = http;
        // this._ndwhConsoleComponent = ndwhConsoleComponent;

    }

    public ngOnInit() {

        this.refreshETLTables();
        // setTimeout(
        //     () => this.loadCT()
        //     , 7000
        // );
    }


    public getDefaultEMR() : void {

        this.getEmr$ = this._emrConfigService.getDefault()
            .subscribe(
                p => {
                    this.emrSystem = p;
                    this.dbProtocol = this.emrSystem.databaseProtocols[0];
                    console.log(this.dbProtocol)
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
                    this.loadingData = false;
                },
                () => {
                    this.refreshETLTables()
                }
            );
    }

    public refreshETLTables(): void {
        var refreshload = setInterval(() => {
            this.barValue = this.barValue+40;

            if (this.barValue >= 100){
                clearInterval(refreshload);
                // this.loadCT() ;

            }
        }, 2000);
        // this.getRefresh$ = this._autoloadService.refreshETL(this.dbProtocol)
        //     .subscribe(
        //         p => {
        //             console.log(p);
        //         },
        //         e => {
        //             this.errorMessage = [];
        //             this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
        //             this.loadingData = false;
        //             console.log(e);
        //
        //         },
        //         () => {
        //             this.moveNext(this.step+1) ;
        //         }
        //     );
    }



    public loadCT(): void {
        this.moveNext(this.step+1) ;

        let element:HTMLElement = document.getElementById('loadchangesfromemr') as HTMLElement;
        element.click();

        var checkLoad = setInterval(() => {
            this.canSend =JSON.parse(localStorage.getItem('canSend'));

            console.log(this.canSend, typeof(this.canSend));

            if (this.canSend == true){
                    // let element:HTMLElement = document.getElementById('sendSmart') as HTMLElement;
                    // element.click();
                // this.loadHTS() ;

                clearInterval(checkLoad);
                }
            }, 3000);

        var checkComplete = setInterval(() => {
            this.ctSendingComplete = JSON.parse(localStorage.getItem('ctSendingComplete'));

            console.log("ctSendingComplete",this.ctSendingComplete, typeof(this.ctSendingComplete));

            if (this.ctSendingComplete == true){
                this.loadHTS() ;
                clearInterval(checkComplete);
            }
        }, 3000);

    }

    public loadHTS(): void {
        this.moveNext(this.step+1) ;
        let element:HTMLElement = document.getElementById('loadHts') as HTMLElement;
        element.click();

        var checkLoad = setInterval(() => {
            this.canSendHts =JSON.parse(localStorage.getItem('canSendHts'));

            console.log(this.canSendHts, typeof(this.canSendHts));

            if (this.canSendHts == true){
                // let element:HTMLElement = document.getElementById('sendHts') as HTMLElement;
                // element.click();
                this.loadMNCH();

                clearInterval(checkLoad);
            }
        }, 2000);

        var checkComplete = setInterval(() => {
            this.htsSendingComplete = JSON.parse(localStorage.getItem('htsSendingComplete'));

            console.log("htsSendingComplete",this.htsSendingComplete, typeof(this.htsSendingComplete));

            if (this.htsSendingComplete == true){
                this.loadMNCH();
                clearInterval(checkComplete);
            }
        }, 3000);
    }

    public loadMNCH(): void {
        this.moveNext(this.step+1) ;
        let element:HTMLElement = document.getElementById('loadMnch') as HTMLElement;
        element.click();

        var checkLoad = setInterval(() => {
            this.canSendMnch =JSON.parse(localStorage.getItem('canSendMnch'));

            console.log(this.canSendMnch, typeof(this.canSendMnch));

            if (this.canSendMnch == true){
                // let element:HTMLElement = document.getElementById('sendMnch') as HTMLElement;
                // element.click();
                // this.loadPREP();

                clearInterval(checkLoad);
            }
        }, 2000);

        var checkComplete = setInterval(() => {
            this.mnchSendingComplete = JSON.parse(localStorage.getItem('mnchSendingComplete'));

            console.log("mnchSendingComplete",this.mnchSendingComplete, typeof(this.mnchSendingComplete));

            if (this.mnchSendingComplete == true){
                this.loadPREP();
                clearInterval(checkComplete);
            }
        }, 3000);
    }

    public loadPREP(): void {
        this.moveNext(this.step+1) ;
        let element:HTMLElement = document.getElementById('loadPrep') as HTMLElement;
        element.click();

        var checkLoad = setInterval(() => {
                this.canSendPrep =JSON.parse(localStorage.getItem('canSendPrep'));

            console.log(this.canSendPrep, typeof(this.canSendPrep));

            if (this.canSendPrep == true){
                // let element:HTMLElement = document.getElementById('sendMnch') as HTMLElement;
                // element.click();
                // this.loadPREP();

                clearInterval(checkLoad);
            }
        }, 2000);
        localStorage.clear();

    }

    public moveNext(num): void {
        this.step = num;
        if (num == 2){
            this.stepOneIsActive = "form-stepper-completed step-section-inactive";
            this.stepTwoIsActive = "form-stepper-active step-section-active";
            // this.loadCT();
        }else if (num == 3){
            this.stepTwoIsActive = "form-stepper-completed step-section-inactive";
            this.stepThreeIsActive = "form-stepper-active step-section-active";
            // this.loadHTS();
        }else if (num == 4){
            this.stepThreeIsActive = "form-stepper-completed step-section-inactive";
            this.stepFourIsActive = "form-stepper-active step-section-active";
            // this.loadMNCH();
        }else if (num == 5){
            this.stepFourIsActive = "form-stepper-completed step-section-inactive";
            this.stepFiveIsActive = "form-stepper-active step-section-active";
            // this.loadPREP();
        }else{
            this.stepFiveIsActive = "form-stepper-end";
        }
    }

    public ngOnDestroy(): void {
        if (this.getEmr$) {
            this.getEmr$.unsubscribe();
        }

    }
}
