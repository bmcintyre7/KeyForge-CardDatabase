import React from 'react';

function createCORSRequest(method, url) {
  var xhr = new XMLHttpRequest();
  if ("withCredentials" in xhr) {
    // XHR for Chrome/Firefox/Opera/Safari.
    xhr.open(method, url, false);
  } else if (typeof XDomainRequest != "undefined") {
    // XDomainRequest for IE.
    xhr = new XDomainRequest();
    xhr.open(method, url);
  } else {
    // CORS not supported.
    xhr = null;
  }
  return xhr;
}

class DetailCardView extends React.Component {
  constructor(props) {
    super(props);
    this.getImageString = this.getImageString.bind(this);
    this.httpGetCard = this.httpGetCard.bind(this);
  }

  getImageString(expansion, number) {
    return '/images/cards/' + expansion + '-' + number + '.jpg';
  }

  httpGetCard(expansion, cardId) {
    let theUrl = 'http://localhost:7230/cards/' + expansion + '/' + cardId;
    let xmlHttp = createCORSRequest('GET', theUrl);
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  render() {
    let cardId = this.props.match.params.cardId;
    let expansionId = this.props.match.params.expansionName;
    let result = this.httpGetCard(expansionId, cardId);
    let theCard = JSON.parse(result);
    return (
      <div>
        <img className={'displayInline'} src={ this.getImageString(expansionId, cardId) } alt={ 'test' } border="5" width='250' height='350'/>
        <div className={'displayInline mx-3'}>
          <div>{theCard.name}</div>
          <div>{theCard.houses[0]}</div>
          {theCard.aember != null &&
            <div>Aember: {theCard.aember}</div>}
          {theCard.power != null &&
            <div>Power: {theCard.power}</div>}
          {theCard.armor != null &&
            <div>Armor: {theCard.armor}</div>}
          <div>{theCard.text}</div>
          <div>{theCard.rarity}</div>
          <div>Artist: {theCard.artist}</div>
        </div>
      </div>
    );
  }
}



export { DetailCardView };
