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

    constructor(public app: AppComponent) {}

    ngOnInit() {
        this.model = [
            {label: 'Dashboard', icon: 'dashboard', routerLink: ['/']},
            {
                label: 'Configuration', icon: 'settings_application',
                items: [
                    {label: 'Registry', icon: 'cloud', routerLink: ['/registryconfig']},
                    {label: 'EMR Settings', icon: 'dvr', routerLink: ['/emrconfig']}
                ]
            }
        ];
    }
}
