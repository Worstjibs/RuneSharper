import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CharacterListComponent } from './character-list/character-list.component';
import { CharacterViewComponent } from './character-view/character-view.component';
import { HomeComponent } from './home/home.component';
import { LineChartComponent } from './line-chart/line-chart.component';
import { CharacterViewResolver } from './_resolvers/character-view.resolver';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'line-chart', component: LineChartComponent },
  { path: 'characters', component: CharacterListComponent },
  { 
    path: 'characters/:userName', 
    component: CharacterViewComponent,
    resolve: {
      character: CharacterViewResolver
    }
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
