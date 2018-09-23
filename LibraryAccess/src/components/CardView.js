import React from 'react';

class CardView extends React.Component {
  constructor(props) {
    super(props)
    this.loadCard = this.loadCard.bind(this);
    this.getImageString = this.getImageString.bind(this);
  }

  getImageString() {
    return 'images/cards/' + this.props.card.set + '-' + this.props.card.number + '.jpg';
  }

  render() {
    return (
      <div>
        {this.props.card.number}
        <br/>
        {this.props.card.text}
        <br/>
        <img src={ this.getImageString(this.props.card.number) } alt={ this.props.card.name } width='250' height='350'/>
      </div>
    );
  }
}

export { CardView };
