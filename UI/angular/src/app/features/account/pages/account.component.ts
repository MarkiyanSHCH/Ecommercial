import { Component } from '@angular/core';
import { ActivatedRoute, Router, Routes } from '@angular/router';

import { RouteGroup } from '../models';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {

  readonly groupedRoutes: RouteGroup[] = <RouteGroup[]> [];

  constructor(
    private _activatedRoute: ActivatedRoute
  ) {
    this.groupedRoutes = this._groupRoutes(this._activatedRoute.routeConfig?.children!);
  }

  getGroupRoutePath(routeGroup: RouteGroup): string {
    return routeGroup.singleRoute ? routeGroup.singleRoute.path! : routeGroup.routes[0].path!;
  }

  private _groupRoutes(routes: Routes): RouteGroup[] {
    return routes
      .reduce((groupedRoutes, iterateRoute) => {
        if (!iterateRoute.data || !iterateRoute.data.group || !iterateRoute.data.name)
          return groupedRoutes;

        const { group, name } = iterateRoute.data;
        const routeGroup = groupedRoutes.find(route => route.name === group);

        if (!routeGroup)
          groupedRoutes.push(<RouteGroup> {
            singleRoute: name === group ? iterateRoute : null,
            name: group,
            routes: name === group ? <Routes> [] : <Routes> [iterateRoute]
          });
        else routeGroup.routes.push(iterateRoute);

        return groupedRoutes;
      }, <RouteGroup[]> []);
  }
}
