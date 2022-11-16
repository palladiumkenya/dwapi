import {Component, OnDestroy, OnInit} from '@angular/core';
import {BreadcrumbService} from '../app/breadcrumb.service';
import {EmrSystem} from '../settings/model/emr-system';
import {AutoloadService} from '../settings/services/autoload.service';
import {ConfirmationService, Message} from 'primeng/api';
import {Subscription} from 'rxjs/Subscription';
import {ProgressBarModule} from 'primeng/progressbar';
import {HttpClient} from "@angular/common/http";


@Component({
    selector: 'liveapp-autoload',
    templateUrl: './autoload.component.html',
    styleUrls: ['./autoload.component.scss']
})
export class AutoloadComponent implements OnInit, OnDestroy {

    private _confirmationService: ConfirmationService;
    private _autoloadService: AutoloadService;

    public getEmr$: Subscription;
    public getMiddleware$: Subscription;
    public emrSystem: EmrSystem;
    public middlewareSystem: EmrSystem;
    public errorMessage: Message[];
    public otherMessage: Message[];
    public loadingData: boolean;

    private _url: string = './api/EmrManager';
    private _http: HttpClient;

    step: number = 1
    stepOneIsActive: string = "form-stepper-active ";
    stepTwoIsActive: string = "form-stepper-unfinished ";
    stepThreeIsActive: string = "form-stepper-unfinished ";
    stepFourIsActive: string = "form-stepper-unfinished ";
    stepFiveIsActive: string = "form-stepper-unfinished ";



    public constructor(public breadcrumbService: BreadcrumbService,
                       confirmationService: ConfirmationService, autoloadService: AutoloadService, http: HttpClient) {
        this.breadcrumbService.setItems([
            {label: ''}
        ]);
        this._confirmationService = confirmationService;
        this._autoloadService = autoloadService;
        this._http = http;

    }

    public ngOnInit() {
        setTimeout(()=>{
            this.refreshETL();
        }, 3000);
    }

    public refreshETL(): void {
        this.getEmr$ = this._autoloadService.refreshETL()
            .subscribe(
                p => {
                    console.log(p);
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error Loading data', detail: <any>e});
                    this.loadingData = false;
                },
                () => {
                    this.moveNext(this.step+1) ;
                }
            );
    }

    public loadCT(): void {
        this.moveNext(this.step+1) ;
    }
    public loadHTS(): void {
        this.moveNext(this.step+1) ;
    }
    public loadMNCH(): void {
        this.moveNext(this.step+1) ;
    }
    public loadPREP(): void {
        this.moveNext(this.step+1) ;
    }

    public moveNext(num): void {
        this.step = num;
        if (num == 2){
            this.stepOneIsActive = "form-stepper-completed";
            this.stepTwoIsActive = "form-stepper-active";
            this.loadCT();
        }else if (num == 3){
            this.stepTwoIsActive = "form-stepper-completed";
            this.stepThreeIsActive = "form-stepper-active";
            this.loadHTS();
        }else if (num == 4){
            this.stepThreeIsActive = "form-stepper-completed";
            this.stepFourIsActive = "form-stepper-active";
            this.loadMNCH();
        }else if (num == 5){
            this.stepFourIsActive = "form-stepper-completed";
            this.stepFiveIsActive = "form-stepper-active";
            this.loadPREP();
        }else{
            this.stepFiveIsActive = "form-stepper-end";
        }
    }

    public ngOnDestroy(): void {
        if (this.getEmr$) {
            this.getEmr$.unsubscribe();
        }
        if (this.getMiddleware$) {
            this.getMiddleware$.unsubscribe();
        }
    }
}
