import { Component, OnInit } from '@angular/core';
import {BreadcrumbService} from '../../app/breadcrumb.service';

@Component({
  selector: 'liveapp-emr-config',
  templateUrl: './emr-config.component.html',
  styleUrls: ['./emr-config.component.scss']
})
export class EmrConfigComponent implements OnInit {

    constructor(private breadcrumbService: BreadcrumbService) {
        this.breadcrumbService.setItems([
            {label: 'Configuration'},
            {label: 'EMR', routerLink: ['/emrconfig']}
        ]);
    }

  ngOnInit() {
  }

}
