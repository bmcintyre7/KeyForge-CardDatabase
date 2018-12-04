import React from 'react';

import {Link} from 'react-router-dom';
import {apiURL, createCORSRequest} from 'shared/createCORSRequest'

class CardView extends React.Component {
  constructor(props) {
    super(props)
    this.getImageString = this.getImageString.bind(this);
    this.httpGetCard = this.httpGetCard.bind(this);
  }

  getImageString(fileName) {
    return '/images/cards/' + fileName + '.jpg';
  }

  httpGetCard() {
    var theUrl = apiURL + '/cards/' + this.props.card;
    var xmlHttp = createCORSRequest('GET', theUrl)
    //xmlHttp.open('GET', theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  render() {
    let theCard = this.props.card;

    let newestExpansion = "";
    let newestImage = "";
    let newestExpansionNumber = "";

    if (null != theCard) {
      newestExpansion = theCard['expansions'][0]['abbreviation'];
      newestImage = theCard['expansions'][0]['imageName'];
      newestExpansionNumber = theCard['expansions'][0]['number'];
      return (
        <div>
          <Link to={'/cards/' + newestExpansion + '/' + newestExpansionNumber}>
            <img className={'imageBorder'} src={ this.getImageString(newestImage) } alt={ 'test' } width='250' height='350'/>
          </Link>
          <br/>
          {theCard.name + " - " + newestExpansion + " #" + newestExpansionNumber}
        </div>
      );
    } else {
      return (
        <div>
          <Link to={'/cards/' + newestExpansion + '/' + newestExpansionNumber}>
            <img className={'imageBorder'} src={ '/images/cards/placeholderCard.png' } alt={ 'test' } width='250' height='350'/>
          </Link>
          <br/>
        </div>
      );
    }
  }
}

export { CardView };
