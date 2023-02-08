import {Component, Input, OnInit} from '@angular/core';
import {AppComponent} from '../app.component';

@Component({
    selector: 'liveapp-app-menu',
    templateUrl: './app-menu.component.html',
    styleUrls: ['./app-menu.component.scss']
})
export class AppMenuComponent implements OnInit {

    @Input() reset: boolean;

    model: any[];

    constructor(public app: AppComponent) {
    }

    ngOnInit() {
        this.model = [
            {label: 'Dashboard', icon: 'pie_chart', routerLink: ['/']},
            {
                label: 'Configuration', icon: 'settings_application',
                items: [
                    {
                        label: 'Registry', icon: 'cloud', items: [
                            {label: 'Care and Treatment', icon: 'cloud', routerLink: ['/registry', 'NDWH']},
                            //{label: 'PSmart', icon: 'credit_card', routerLink: ['/registry', 'PSMART']},
                            {label: 'PKV Services', icon: 'search', routerLink: ['/registry', 'CBS']},
                            {label: 'HIV Testing Services', icon: 'local_hospital', routerLink: ['/registry', 'HTS']},
                            // {label: 'Migration Services', icon: 'flight_takeoff', routerLink: ['/registry', 'MGS']}
                            {label: 'MNCH Services', icon: 'pregnant_woman', routerLink: ['/registry', 'MNCH']},
                            {label: 'PrEP Services', icon: 'crop_square', routerLink: ['/registry', 'PREP']},
                            {label: 'Client Registry Services', icon: 'credit_card', routerLink: ['/registry', 'CRS']}
                        ]
                    },
                    {label: 'EMR Settings', icon: 'dvr', routerLink: ['/emrconfig']}
                ]
            },
            {
                label: 'Dockets', icon: 'dashboard',
                items: [
                    { label: 'Care and Treatment', icon: 'cloud', routerLink: ['/datawarehouse']},
                    //{label: 'PSmart', icon: 'credit_card', routerLink: ['/psmart']},
                    {label: 'PKV Services', icon: 'search', routerLink: ['/cbs']},
                    {label: 'HIV Testing Services', icon: 'local_hospital', routerLink: ['/hts']},
                    // {label: 'Migration Services', icon: 'flight_takeoff', routerLink: ['/mgs']}
                    {label: 'MNCH Services', icon: 'pregnant_woman', routerLink: ['/mnch']},
                    {label: 'PrEP Services', icon: 'crop_square', routerLink: ['/prep']},
                    {label: 'Client Registry Services', icon: 'credit_card', routerLink: ['/crs']}
                ]
            },
            { label: 'Exports', icon: 'folder', routerLink: ['/exports'] },
            { label: 'Upload', icon: 'folder', routerLink: ['/upload'] },

            {
                label: 'Merged Dockets', icon: 'cloud', routerLink: ['/autoload']
            }
        ];
    }
}
