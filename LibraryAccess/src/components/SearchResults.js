import React from 'react';

import {CardView} from 'components/CardView';
import {apiURL, createCORSRequest} from 'shared/createCORSRequest';
import {PageHeader} from "components/PageHeader";
import {PageFooter} from "components/PageFooter";
import {Link} from "react-router-dom";
import axios from "axios";

class SearchResults extends React.Component {
  state = {
    searchResults: [],
    doneSearching: false
  }

  constructor(props) {
    super(props)
    this.getImageString = this.getImageString.bind(this)
    this.httpGetCards = this.httpGetCards.bind(this);
  }

  componentWillMount() {
    this.httpGetCards();
  }

  componentDidMount() {
    window.scrollTo(0,0);
  }

  getImageString(houseName) {
    return '/images/houses/' + houseName + '.png';
  }

  httpGetCards() {
    //var theUrl = apiURL + '/cardCount' + this.props.location.state.query;
    //axios.get(theUrl).then(newData => {
    //  this.setState({countResults: newData.data});
    //});

    var theUrl = apiURL + '/cards' + this.props.location.state.query;
    axios.get(theUrl).then(newData => {
      this.setState({searchResults: newData.data});
      this.setState({doneSearching: true});
    });
  }

  render() {
    var display = new Array();

    if (!this.state.doneSearching)
      display.push(<div><img src={'/images/loading/page-turn.gif'} width={"50"} height={"50"}></img><br/>Loading Results...</div>);
    else if (this.state.searchResults.length == 0)
      display.push(<div>No Results Found :(</div>);

    for (var i = 0; i < this.state.searchResults.length; i++) {
      display.push((
        <div key={'card-' + this.state.searchResults[i]['name']} className='displayInline mx-3 my-3'><CardView
          card={this.state.searchResults[i]}/></div>));
    }

    return (
      <div>
        <PageHeader numResults={this.state.searchResults.length ? this.state.searchResults.length : null}/>
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
