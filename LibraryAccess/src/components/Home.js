import React from 'react';

import {Redirect} from 'react-router-dom';
import {DropdownButton, MenuItem} from 'react-bootstrap'
import {AutoComplete} from 'components/AutoComplete';
import {PageHeader} from './PageHeader'
import {PageFooter} from './PageFooter'
import {createCORSRequest, apiURL} from 'shared/createCORSRequest';
import IconTitle, { ICONS } from 'shared/IconTitle';

class Home extends React.Component {
  state = {
    toResults: false
  }

  searchQueryString = '';

  constructor(props) {
    super(props)
    this.getImageString = this.getImageString.bind(this)
    this.httpGetHouses = this.httpGetHouses.bind(this);
    this.httpGetKeywords = this.httpGetKeywords.bind(this);
    this.httpGetTypes = this.httpGetTypes.bind(this);
    this.httpGetTraits = this.httpGetTraits.bind(this);
    this.makeTextSearchField = this.makeTextSearchField.bind(this);
    this.makeComparisonSearchField = this.makeComparisonSearchField.bind(this);
    this.makeChecklistSearchField = this.makeChecklistSearchField.bind(this);
    this.doSearch = this.doSearch.bind(this);
    this.slugify = this.slugify.bind(this);
    this.checkEnter = this.checkEnter.bind(this);
  }

  componentDidMount() {
    window.scrollTo(0, 0);
  }

  getImageString(houseName) {
    return '/images/houses/' + houseName + '.png';
  }

  httpGetHouses() {
    var theUrl = apiURL + '/houses';
    var xmlHttp = createCORSRequest('GET', theUrl)
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  httpGetTraits() {
    var theUrl = apiURL + '/traits';
    var xmlHttp = createCORSRequest('GET', theUrl)
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  httpGetKeywords() {
    var theUrl = apiURL + '/keywords';
    var xmlHttp = createCORSRequest('GET', theUrl)
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  httpGetTypes() {
    var theUrl = apiURL + '/types';
    var xmlHttp = createCORSRequest('GET', theUrl)
    xmlHttp.send(null);
    return xmlHttp.responseText;
  }

  // Event fired when the user presses a key down
  checkEnter = e => { {
    const userInput = this.state.userInput;
    // Update the user input and reset the rest of the state
    if (e.keyCode === 13) {
      this.doSearch();
    }
  } }

  makeTextSearchField(icon, label) {
    let slugifiedLabel = this.slugify(label);

    return (
      <div className='row h-100 justify-content-center align-items-center'>
        <div className='col-1'/>
        <div className='col-2'>
          <div className='searchLabel float-right'>
            {icon}
          </div>
        </div>
        <div className='col-6 text-center displayInline mx-0 px-0'>
          <div className='px-0 mx-0 minWidth-400'>
            <input type='text typeahead' data-provide='testData' className='form-control'
                   id={slugifiedLabel + '_Value'} onKeyDown={this.checkEnter}/>
          </div>
        </div>
        <div className='col-2'/>
      </div>
    );
  }

  makeAutoCompleteSearchField(icon, label, suggestions, checkboxLabel) {
    let slugifiedLabel = this.slugify(label);

    return (
      <div className='row h-100 justify-content-center align-items-center'>
        <div className='col-1'/>
        <div className='col-2'>
          <div className='searchLabel float-right'>
            {icon}
          </div>
        </div>
        <div className='col-6 text-center displayInline mx-0 px-0'>
          <div className='px-0 mx-0 minWidth-400'>
            <AutoComplete suggestions={suggestions} id={slugifiedLabel + '_Value'}/>
            <div key={'checkbox-' + slugifiedLabel} className='mx-2 px-0 displayInline'>
              <label className='align-middle m-0 p-0'>
                <input className='align-middle mr-1' type='checkbox' value='' id={slugifiedLabel + '_checkbox_Value'}/>
                {checkboxLabel}
              </label>
            </div>
          </div>
        </div>
        <div className='col-2'/>
      </div>
    );
  }


  slugify(text) {
    return text.toString().toLowerCase()
      .replace(/\s+/g, '-')           // Replace spaces with -
      .replace(/[^\w\-]+/g, '')       // Remove all non-word chars
      .replace(/\-\-+/g, '-')         // Replace multiple - with single -
      .replace(/^-+/, '')             // Trim - from start of text
      .replace(/-+$/, '');            // Trim - from end of text
  }

  makeComparisonSearchField(icon, label) {
    let slugifiedLabel = this.slugify(label);
    return (
      <div className='row h-100 justify-content-center align-items-center'>
        <div className='col-1'/>
        <div className='col-2'>
          <div className='searchLabel float-right'>
            {icon}
          </div>
        </div>
        <div className='col-6 text-center mx-0 px-0'>
          <div className='px-0 mx-0 minWidth-400'>
            <div className='row'>
              <div className='col-4'>
                <select id={slugifiedLabel + '_Select'} className='custom-select form-control mt-0'>
                  <option defaultValue value='='>{'='}</option>
                  <option value=">=">{'>='}</option>
                  <option value="<=">{'<='}</option>
                  <option value=">">{'>'}</option>
                  <option value="<">{'<'}</option>
                </select>
              </div>
              <div className='col-8 mx-0 my-0 py-0'>
                <input type='text' className='form-control' id={slugifiedLabel + '_Value'}/>
              </div>
            </div>
          </div>
        </div>
        <div className='col-2'/>
      </div>
    );
  }

  makeChecklistSearchField(icon, label, options, disclaimer) {
    let slugifiedLabel = this.slugify(label);
    var display = new Array();
    for (var i = 0; i < options.length; ++i) {
      let slugifiedOption = this.slugify(options[i]);

      display.push((
        //<Link to={'/cards/house/' + houses[i]}>
        <div key={'checkbox-' + slugifiedLabel + '-' + i} className='mx-2 px-0 displayInline'>
          <label className='align-middle m-0 p-0'><input className='align-middle mr-2' type='checkbox' value=''
                                                         id={slugifiedOption + '_Value'}/>{options[i]}</label>
        </div>
        //</Link>
      ));
    }

    return (
      <div className='row h-100 justify-content-center align-items-center'>
        <div className='col-1'/>
        <div className='col-2'>
          <div className='searchLabel float-right'>
            {icon}
          </div>
        </div>
        <div className='col-6 text-center displayInline mx-0 px-0'>
          <div className='px-0 mx-0 py-2 rounded minWidth-400'>
            {display}
            <br/>
            <span className='tinyLabel text-center mx-3'>{disclaimer}</span>
          </div>
        </div>
        <div className='col-2'/>
      </div>
    );
  }

  doSearch() {
    var queryString = '?';
    var keywordNames = this.httpGetKeywords().split(', ');
    var houseNames = this.httpGetHouses().split(', ');
    var typeNames = this.httpGetTypes().split(', ');
    var name = $('#name_Value').val();
    var text = $('#text_Value').val();
    var artist = $('#artist_Value').val();
    var traits = $('#traits_Value').val();
    var traitsOr = $('#traits_checkbox_Value').is(':checked');
    var houses = new Array();
    for (var i = 0; i < houseNames.length; ++i) {
      var value = $('#' + this.slugify(houseNames[i]) + '_Value').is(':checked');
      if (value)
        houses.push(houseNames[i]);
    }

    //houses.set('Brobnar', $('#brobnar_Value').is(":checked"));
    //houses.set('Dis', $('#dis_Value').is(":checked"));
    //houses.set('Logos', $('#logos_Value').is(":checked"));
    //houses.set('Mars', $('#mars_Value').is(":checked"));
    //houses.set('Sanctum', $('#sanctum_Value').is(":checked"));
    //houses.set('Shadows', $('#shadows_Value').is(":checked"));
    //houses.set('Untamed', $('#untamed_Value').is(":checked"));
    var rarities = new Array();
    if ($('#common_Value').is(':checked'))
      rarities.push('Common');
    if ($('#uncommon_Value').is(':checked'))
      rarities.push('Uncommon');
    if ($('#rare_Value').is(':checked'))
      rarities.push('Rare');
    if ($('#special_Value').is(':checked'))
      rarities.push('Special');

    var types = new Array();
    for (var i = 0; i < typeNames.length; ++i) {
      var value = $('#' + this.slugify(typeNames[i]) + '_Value').is(':checked');
      if (value)
        types.push(typeNames[i]);
    }

    var aember = new Array(
      $('#aember_Select').val(),
      $('#aember_Value').val()
    );
    var power = new Array(
      $('#power_Select').val(),
      $('#power_Value').val()
    );
    var armor = new Array(
      $('#armor_Select').val(),
      $('#armor_Value').val()
    );
    var keywords = new Array();
    for (var i = 0; i < keywordNames.length; ++i) {
      var value = $('#' + this.slugify(keywordNames[i]) + '_Value').is(':checked');
      if (value)
        keywords.push(keywordNames[i]);
    }
    // TODO: Traits since those should be tag style input and that's not implemented yet

    if (name != '')
      queryString += (queryString.length != 1 ? '&' : '') + 'name=' + name;

    if (text != '')
      queryString += (queryString.length != 1 ? '&' : '') + 'text=' + text;

    if (artist != '')
      queryString += (queryString.length != 1 ? '&' : '') + 'artist=' + artist;

    if (traits != '') {
      queryString += (queryString.length != 1 ? '&' : '') + 'traits=' + traits;
      queryString += (queryString.length != 1 ? '&' : '') + 'traitsOr=' + (!traitsOr ? 'true' : 'false');
    }

    for (var i = 0; i < keywords.length; ++i)
      queryString += (queryString.length != 1 ? '&' : '') + 'keywords=' + keywords[i];

    for (var i = 0; i < houses.length; ++i)
      queryString += (queryString.length != 1 ? '&' : '') + 'houses=' + houses[i];

    for (var i = 0; i < rarities.length; ++i)
      queryString += (queryString.length != 1 ? '&' : '') + 'rarities=' + rarities[i];

    for (var i = 0; i < types.length; ++i)
      queryString += (queryString.length != 1 ? '&' : '') + 'types=' + types[i];

    if (aember[1] != '') {
      queryString += (queryString.length != 1 ? '&' : '') + 'aember=' + aember[0] + aember[1];
    }

    if (power[1] != '') {
      queryString += (queryString.length != 1 ? '&' : '') + 'power=' + power[0] + power[1];
    }

    if (armor[1] != '') {
      queryString += (queryString.length != 1 ? '&' : '') + 'armor=' + armor[0] + armor[1];
    }

    this.searchQueryString = queryString;

    this.setState({toResults: true});
  }

  render() {
    var houses = this.httpGetHouses().split(', ');
    var displayHouses = new Array();
    for (var i = 0; i < houses.length; ++i)
      displayHouses.push((
        //<Link to={'/cards/house/' + houses[i]}>
        <div key={'houses-' + i} className='displayInline maxWidth-100'>
          <input type='checkbox' value='' id={this.slugify(houses[i]) + '_Value'}/><img className='mx-1 smallHouseLogo'
                                                                                        src={this.getImageString(houses[i])}/>
        </div>
        //</Link>
      ));
    var keywords = this.httpGetKeywords().split(', ');
    keywords.sort();

    var types = this.httpGetTypes().split(', ');
    types.sort();

    var traits = this.httpGetTraits().split(', ');
    traits.sort();

    if (true == this.state.toResults)
      return <Redirect push to={{
        pathname: '/searchResults',
        state: {query: this.searchQueryString}
      }}/>

    return (
      <div className='justify-content-center align-items-center text-center'>
        <PageHeader />
        <div className='fluid-container h-100 displayInline px-4'>
          <div className='row h-100 justify-content-center align-items-center'>
            <div className='col-3'/>
            <div className='col-6 text-center pr-5'>
              {this.makeTextSearchField(<IconTitle faIcon={ICONS.ID_CARD} title='Name' />, 'Name')}
              <br/>
              <div className='row h-100 justify-content-center align-items-center'>
                <div className='col-1'/>
                <div className='col-2'>
                  <div className='searchLabel float-right'>
                    <IconTitle faIcon={ICONS.HOME} title='Houses' solid />
                  </div>
                </div>
                <div className='col-6 text-center displayInline mx-0 px-0'>
                  <div className='p-0 m-0 rounded minWidth-400'>
                    {displayHouses}
                    <br/>
                    <span className='tinyLabel text-center mx-3'>{'Select each house that the results may be.'}</span>
                  </div>
                </div>
                <div className='col-2'/>
              </div>
              <br/>
              {this.makeChecklistSearchField(<IconTitle faIcon={ICONS.FINGERPRINT} title='Type' solid />, 'Type', types, 'Select each type the results may be.')}
              <br/>
              {this.makeTextSearchField(<IconTitle faIcon={ICONS.ALIGN_JUSTIFY} title='Text' solid />, 'Text')}
              <br/>
              {this.makeComparisonSearchField(<IconTitle faIcon={ICONS.GEM} title='Æmber' />, 'Aember')}
              <br/>
              {this.makeComparisonSearchField(<IconTitle faIcon={ICONS.DUMBBELL} title='Power' solid />, 'Power')}
              <br/>
              {this.makeComparisonSearchField(<IconTitle faIcon={ICONS.SHIELD_ALT} title='Armor' solid />, 'Armor')}
              <br/>
              {this.makeChecklistSearchField(<IconTitle faIcon={ICONS.KEY} title='Keywords' solid />, 'Keywords', keywords, 'Select each keyword that the results must have.')}
              <br/>
              {this.makeAutoCompleteSearchField(<IconTitle faIcon={ICONS.LIST_UI} title='Traits' solid />, 'Traits', traits, 'Results must have all entered traits')}
              <br/>
              {this.makeChecklistSearchField(<IconTitle faIcon={ICONS.STAR} title='Rarity' />, 'Rarity', ['Common', 'Uncommon', 'Rare', 'Special'], 'Select each rarity that the results may be.')}
              <br/>
              {this.makeTextSearchField(<IconTitle faIcon={ICONS.PAINT_BRUSH} title='Artist' solid />, 'Artist')}
              <br/>
              <div className={'row h-100 justify-content-center align-items-center'}>
                <div className={'col-3'} />
                <button className='btn' id={'search'} onClick={this.doSearch}>Search</button>
              </div>
            </div>
            <div className='col-3'/>
          </div>
        </div>
        <PageFooter/>
      </div>
    );
  }
}

export {Home, apiURL};
