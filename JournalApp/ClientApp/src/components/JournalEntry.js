import React, { Component } from "react";
import "./JournalEntry.css";

export class JournalEntry extends Component {
    constructor(props) {
        super(props);
        this.state = {
            prompt: "How was your day? Are you feeling well?",
            journalEntries: [],
            currentEntry: "",
            tempPrompt: ""
        };
        fetch('api/journal/GetAllEntries')
            .then(response => response.json())
            .then(data => {
                localStorage.setItem("journalEntries", JSON.stringify(data));
                this.setState({ journalEntries: data });
            }).catch(error => console.log(error)
            );

        const prompt = localStorage.getItem("prompt");
        if (prompt == null) {
            fetch('api/journal/GetPrompt')
                .then(response => response.text()) // .json() to get a load of JSON, .text() to get a plain string. 
                .then(data => {
                    console.log(data);
                    localStorage.setItem("prompt", data);
                    this.setState({ prompt: data });
                })
                .catch(error => {
                    console.log(error);
                });
        } else {
            this.setState({ prompt: prompt });
        }
    }
    setPrompt = () => {
        const prompt = this.state.tempPrompt;
        this.setState({ prompt: prompt });
        localStorage.setItem("prompt", prompt);
    };
    handleChange = event => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    };
    writeJournalEntry = () => {
        const entry = this.state.currentEntry;
        const entryObject = { timeStamp: new Date().toString(), actualText: entry };
        const newJournalEntries = [entryObject, ...this.state.journalEntries];
        this.setState({ currentEntry: "", journalEntries: newJournalEntries });
        localStorage.setItem("journalEntries", JSON.stringify(newJournalEntries));
    };
    render() {
        return (
            <div>
                <label>
                    Set your prompt here. <input type="text" name="tempPrompt" onChange={this.handleChange} />
                    <button type="submit" onClick={this.setPrompt}>
                        Set Prompt
          </button>
                </label>
                <br />
                <label>
                    Journal Entry
          <textarea
                        value={this.state.currentEntry}
                        onChange={this.handleChange}
                        name="currentEntry"
                        id="journalInput"
                        rows="5"
                        cols="30"
                        placeholder={this.state.prompt}
                    />
                    <button type="submit" onClick={this.writeJournalEntry}>
                        Add Journal Entry
          </button>
                </label>
                <ul id="fullJournal">
                    {this.state.journalEntries.map(entry => (
                        <li key={entry.timeStamp}>
                            <p>Time: {entry.timeStamp}</p>
                            <p>{entry.actualText}</p>
                        </li>
                    ))}
                </ul>
            </div>
        );
    }
}
