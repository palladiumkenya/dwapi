import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Subscription} from 'rxjs/Subscription';

@Component({
  selector: 'liveapp-registry-manager',
  templateUrl: './registry-manager.component.html',
  styleUrls: ['./registry-manager.component.scss']
})
export class RegistryManagerComponent implements OnInit, OnDestroy {

    private get$: Subscription;
    public docketId: string;
    public userId: string;


    public constructor(private route: ActivatedRoute) {
    }

    public ngOnInit() {
        this.get$ = this.route.params.subscribe(params => {
                this.docketId = params['docketId'];
                console.log(this.docketId);
            }
        );
    }

    public ngOnDestroy() {
        if (this.get$) {
            this.get$.unsubscribe();
        }
    }

}
