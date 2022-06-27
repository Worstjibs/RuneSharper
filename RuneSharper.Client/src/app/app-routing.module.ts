import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CharacterListComponent } from './character-list/character-list.component';
import { HomeComponent } from './home/home.component';
import { LineChartComponent } from './line-chart/line-chart.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'line-chart', component: LineChartComponent },
  { path: 'characters', component: CharacterListComponent }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
