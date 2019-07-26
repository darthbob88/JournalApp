import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { JournalEntry } from './components/JournalEntry';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={JournalEntry} />
            <Route path='/journal' component={JournalEntry} />
      </Layout>
    );
  }
}
