import {AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {AppComponent} from '../app.component';
declare var jQuery: any;

@Component({
  selector: 'liveapp-rightpanel',
  templateUrl: './rightpanel.component.html',
  styleUrls: ['./rightpanel.component.scss']
})
export class RightpanelComponent implements OnDestroy, AfterViewInit {

    rightPanelMenuScroller: HTMLDivElement;

    @ViewChild('rightPanelMenuScroller') rightPanelMenuScrollerViewChild: ElementRef;

    constructor(public app: AppComponent) {}

    ngAfterViewInit() {
        this.rightPanelMenuScroller = <HTMLDivElement> this.rightPanelMenuScrollerViewChild.nativeElement;

        setTimeout(() => {
            jQuery(this.rightPanelMenuScroller).nanoScroller({flash: true});
        }, 10);
    }

    ngOnDestroy() {
        jQuery(this.rightPanelMenuScroller).nanoScroller({flash: true});
    }
}
