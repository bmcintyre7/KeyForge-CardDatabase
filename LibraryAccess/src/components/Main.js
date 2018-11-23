require('normalize.css/normalize.css');
require('styles/App.css');

import React from 'react';
const supportsHistory = 'pushState' in window.history
import {BrowserRouter as Router, Route, Switch, BrowserHistory} from 'react-router-dom';
//import { Home } from './OldHome'
import { DetailCardView } from './DetailCardView'
import { ExpansionView } from './ExpansionView'
import { Home} from './Home'
import { SearchResults} from './SearchResults';

class AppController extends React.Component {
  render() {
    //if (document.cookie.includes('ComicBoxOnline')) {
    //  loggedIn = true;
    //  fbInfo = new Array();
    //  fbInfo.email = getCookie('ComicBoxOnline');
    //  //console.log(fbInfo.email);
    //}

    //<Route path='/addBook' component={AddBook}/>
    //<Route path='/editBook/' component={EditBook}/>
    //<Route path='/bulkAddBooks/' component={BulkAddBooks}/>
    //<Route path='/walkthrough/' component={Walkthrough}/>

    return (
      <Router>
        <Switch>
          <Route exact path='/' component={Home}/>
          <Route exact path='/cards/:expansionName' component={ExpansionView}/>
          <Route exact path='/cards/:expansionName/:cardId' component={DetailCardView}/>
          <Route exact path='/searchResults' component={SearchResults}/>
        </Switch>
      </Router>
    );
  }
}

//AppController.defaultProps = {
//};

export default AppController;
