import {Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges} from '@angular/core';
import {BreadcrumbService} from '../../../app/breadcrumb.service';
import {RegistryConfigService} from '../../services/registry-config.service';
import {Subscription} from 'rxjs/Subscription';
import {CentralRegistry} from '../../model/central-registry';
import {ConfirmationService, Message} from 'primeng/primeng';
import {VerificationResponse} from '../../model/verification-response';

@Component({
  selector: 'liveapp-registry-config',
  templateUrl: './registry-config.component.html',
  styleUrls: ['./registry-config.component.scss']
})
export class RegistryConfigComponent implements OnInit, OnDestroy , OnChanges {

    @Input() docketId: string;
    private _registryConfigService: RegistryConfigService;
    private _confirmationService: ConfirmationService;
    public loadingData: boolean;

    public getDefault$: Subscription;
    public get$: Subscription;
    public saveDefault$: Subscription;
    public verfiy$: Subscription;

    public centralRegistry: CentralRegistry = {subscriberId: 'DWAPI'};
    public canSave: boolean;
    public canVerfiy: boolean;
    public isVerfied: VerificationResponse;

    public errorMessage: Message[];
    public otherMessage: Message[];

    public constructor(public breadcrumbService: BreadcrumbService,
                       confirmationService: ConfirmationService, registryConfigService: RegistryConfigService) {
        this.breadcrumbService.setItems([
            {label: 'Configuration'},
            {label: 'Registry', routerLink: ['/registryconfig']}
        ]);
        this._confirmationService = confirmationService;
        this._registryConfigService = registryConfigService;
    }

    public ngOnInit() {
        this.loadingData = true;
    }

    ngOnChanges(changes: SimpleChanges): void {
        this.loadData();
    }

    public loadData(): void {
        this.centralRegistry = {subscriberId: 'DWAPI'};
        this.getDefault$ = this._registryConfigService.get(this.docketId)
            .subscribe(
                p => {
                    this.centralRegistry = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push(this.getServerity(e));
                    this.loadingData = false;
                },
                () => {
                    this.loadingData = false;
                }
            );
    }

    public saveDefault(): void {
        this.errorMessage = [];
        this.otherMessage = [];
        this.saveDefault$ = this._registryConfigService.saveDefault(this.centralRegistry)
            .subscribe(
                p => {
                    // this.centralRegistry = p;
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error saving ', detail: <any>e});
                },
                () => {
                    this.otherMessage = [];
                    this.otherMessage.push({severity: 'success', detail: 'saved successfully'});
                    this.loadData();
                }
            );
    }

    public verfiy(): void {
        this.errorMessage = [];
        this.otherMessage = [];
        this.verfiy$ = this._registryConfigService.verify(this.centralRegistry)
            .subscribe(
                p => {
                    this.isVerfied = p;
                    console.log(p);
                    if (this.isVerfied) {
                        this.centralRegistry.name = this.isVerfied.registryName;
                    }
                },
                e => {
                    this.errorMessage = [];
                    this.errorMessage.push({severity: 'error', summary: 'Error verifying ', detail: <any>e});
                },
                () => {
                    this.errorMessage = [];
                    this.otherMessage = [];
                    if (this.isVerfied.verified) {
                        this.otherMessage.push({severity: 'success', detail: 'url has been successfull verified !'});
                        this.errorMessage.push({severity: 'success', summary: 'connection was successful '});
                    } else {
                        this.errorMessage.push({severity: 'error', summary: 'url cannot be verfied '});
                    }
                }
            );
    }

    public ngOnDestroy(): void {
        if (this.getDefault$) {
            this.getDefault$.unsubscribe();
        }
        if (this.saveDefault$) {
            this.saveDefault$.unsubscribe();
        }
        if (this.verfiy$) {
            this.verfiy$.unsubscribe();
        }
    }

    private getServerity(e: string): Message {
        if (e.indexOf('no record(s)') >= 0) {
            return {severity: 'warn', detail: 'Registry is not setup'};
        } else {
            return {severity: 'error', detail: 'Error reading data '};
        }
    }
}
