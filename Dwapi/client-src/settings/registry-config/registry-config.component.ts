import { Component, OnInit } from '@angular/core';
import {BreadcrumbService} from '../../app/breadcrumb.service';

@Component({
  selector: 'liveapp-registry-config',
  templateUrl: './registry-config.component.html',
  styleUrls: ['./registry-config.component.scss']
})
export class RegistryConfigComponent implements OnInit {

    constructor(private breadcrumbService: BreadcrumbService) {
        this.breadcrumbService.setItems([
            {label: 'Configuration'},
            {label: 'Registry', routerLink: ['/controlpanel']}
        ]);
    }

  ngOnInit() {
  }

}
