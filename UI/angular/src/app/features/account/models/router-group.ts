import { Routes, Route } from '@angular/router';

export interface RouteGroup {
    singleRoute: Route;
    name: string;
    routes: Routes;
}
