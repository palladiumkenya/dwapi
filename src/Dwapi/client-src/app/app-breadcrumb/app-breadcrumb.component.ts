import {Component, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from 'rxjs/Subscription';
import {MenuItem} from 'primeng/api';
import {BreadcrumbService} from '../breadcrumb.service';

@Component({
  selector: 'liveapp-app-breadcrumb',
  templateUrl: './app-breadcrumb.component.html',
  styleUrls: ['./app-breadcrumb.component.scss']
})
export class AppBreadcrumbComponent implements OnInit, OnDestroy {


    subscription: Subscription;

    items: MenuItem[];

    constructor(public breadcrumbService: BreadcrumbService) {
        this.subscription = breadcrumbService.itemsHandler.subscribe(response => {
            this.items = response;
        });
    }

    ngOnInit(): void {
    }

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }
}
