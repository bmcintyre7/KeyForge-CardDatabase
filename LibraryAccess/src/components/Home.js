import React from 'react';

import {Redirect} from 'react-router-dom';
import {DropdownButton, MenuItem} from 'react-bootstrap'
import {AutoComplete} from 'components/AutoComplete';
import {PageHeader} from './PageHeader'

let apiURL = 'http://142.93.181.3:7001';

function createCORSRequest(method, url) {
  var xhr = new XMLHttpRequest();
  if ('withCredentials' in xhr) {
    // XHR for Chrome/Firefox/Opera/Safari.
    xhr.open(method, url, false);
  } else if (typeof XDomainRequest != 'undefined') {
    // XDomainRequest for IE.
    xhr = new XDomainRequest();
    xhr.open(method, url);
  } else {
    // CORS not supported.
    xhr = null;
  }
  return xhr;
}

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
                   id={slugifiedLabel + '_Value'}/>
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
              <label className='align-middle m-0 p-0'><input className='align-middle mr-1' type='checkbox' value=''
                                                             id={slugifiedLabel + '_checkbox_Value'}/>{checkboxLabel}
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

  doSearch(user) {
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
      console.log(traitsOr);
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

    this.searchQueryString = queryString;

    console.log(rarities.length);

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

    console.log(this.state.toResults);

    if (true == this.state.toResults)
      return <Redirect push to={{
        pathname: '/searchResults',
        state: {query: this.searchQueryString}
      }}/>

    return (
      <div className='justify-content-center align-items-center text-center'>
        <PageHeader />
        <div className='fluid-container h-100 m-5 displayInline px-4 pb-2'>
          <div className='row h-100 justify-content-center align-items-center'>
            <div className='col-3'/>
            <div className='col-6 text-center'>
              {this.makeTextSearchField(<span className='far fa-id-card fa-sm'><div className={'searchLabel displayInline pl-2'}>Name:</div></span>, 'Name')}
              <br/>
              <div className='row h-100 justify-content-center align-items-center'>
                <div className='col-1'/>
                <div className='col-2'>
                  <div className='searchLabel float-right'>
                    <span className='fas fa-home fa-sm'><div className={'searchLabel displayInline pl-2'}>Houses:</div></span>
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
              {this.makeChecklistSearchField(<span className='fas fa-fingerprint fa-sm'><div className={'searchLabel displayInline pl-2'}>Type:</div></span>, 'Type', types, 'Select each type the results may be.')}
              <br/>
              {this.makeTextSearchField(<span className='fas fa-align-justify fa-sm'><div className={'searchLabel displayInline pl-2'}>Text:</div></span>, 'Text')}
              <br/>
              {this.makeComparisonSearchField(<span className='far fa-gem fa-sm'><div className={'searchLabel displayInline pl-2'}>Aember:</div></span>, 'Aember')}
              <br/>
              {this.makeComparisonSearchField(<span className='fas fa-dumbbell fa-sm'><div className={'searchLabel displayInline pl-2'}>Power:</div></span>, 'Power')}
              <br/>
              {this.makeComparisonSearchField(<span className='fas fa-shield-alt fa-sm'><div className={'searchLabel displayInline pl-2'}>Armor:</div></span>, 'Armor')}
              <br/>
              {this.makeChecklistSearchField(<span className='fas fa-key fa-sm'><div className={'searchLabel displayInline pl-2'}>Keywords:</div></span>, 'Keywords', keywords, 'Select each keyword that the results must have.')}
              <br/>
              {this.makeAutoCompleteSearchField(<span className='fas fa-list-ul fa-sm'><div className={'searchLabel displayInline pl-2'}>Traits:</div></span>, 'Traits', traits, 'Results must have all entered traits')}
              <br/>
              {this.makeChecklistSearchField(<span className='far fa-star fa-sm'><div className={'searchLabel displayInline pl-2'}>Rarity:</div></span>, 'Rarity', ['Common', 'Uncommon', 'Rare', 'Special'], 'Select each rarity that the results may be.')}
              <br/>
              {this.makeTextSearchField(<span className='fas fa-paint-brush fa-sm'><div className={'searchLabel displayInline pl-2'}>Artist:</div></span>, 'Artist')}
              <br/>
              <div className={'row h-100 justify-content-center align-items-center'}>
                <div className={'col-3'} />
                <button className='btn' onClick={this.doSearch}>Search</button>
              </div>
            </div>
            <div className='col-3'/>
          </div>
        </div>
      </div>
    );
  }
}

export {Home, apiURL};
