import React from 'react';
import { Route, Switch } from 'react-router-dom';
import FirstPage from '../views/FirstPage';
import Home from '../views/Home';
import NotFound from '../views/NotFound';
import SecondPage from '../views/SecondPage';

export default function Routes() {
  return (
    <div>
      <Switch>
        <Route exact path='/' component={Home}/>
        <Route path='/first' component={FirstPage}/>
        <Route path='/second' component={SecondPage}/>
        <Route path='*' component={NotFound} />
      </Switch>
    </div>
  );
}
