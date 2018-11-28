import React from 'react';

import {CardView} from 'components/CardView';
import {apiURL, createCORSRequest} from 'shared/createCORSRequest';
import {PageHeader} from "components/PageHeader";
import {PageFooter} from "components/PageFooter";
import {Link} from "react-router-dom";

class SearchResults extends React.Component {
  constructor(props) {
    super(props)
    this.getImageString = this.getImageString.bind(this)
    this.httpGetCards = this.httpGetCards.bind(this);
  }

  componentDidMount() {
    window.scrollTo(0,0);
  }

  getImageString(houseName) {
    return '/images/houses/' + houseName + '.png';
  }

  httpGetCards() {
    var theUrl = apiURL + '/cards' + this.props.location.state.query;
    var xmlHttp = createCORSRequest('GET', theUrl);
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  render() {
    var searchResults = JSON.parse(this.httpGetCards());

    console.log(searchResults);
    console.log(searchResults.length);
    var display = new Array();

    if (searchResults.length == 0)
      display.push(<div>No results found :(</div>);

    for (var i = 0; i < searchResults.length; i++) {
      display.push((
        <div key={'card-' + searchResults[i]['name']} className='displayInline mx-3 my-3'><CardView card={searchResults[i]}/></div>));
    }

    return (
      <div>
        <PageHeader numResults={searchResults.length}/>
        <div className='row h-100 justify-content-center align-items-center'>
          <div className='col-2'/>
          <div className='col-8 text-center' >
            { display }
            <br/><br/>
            <Link to={'/'} style={{ textDecoration: 'none', color: 'lightblue' }}>Search Again</Link>
          </div>
          <div className='col-2'/>
        </div>
        <PageFooter/>
      </div>
    );
  }
}

export { SearchResults };
