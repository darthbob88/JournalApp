import React, { Component } from "react";
import "./JournalEntry.css";

export class JournalEntry extends Component {
    constructor(props) {
        super(props);
        this.state = {
            prompt: "How was your day? Are you feeling well?",
            journalEntries: [],
            currentEntry: ""
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
                .then(response => response.text())
                .then(data => {
                    console.log(data);
                    localStorage.setItem("prompt", data);
                    this.setState({ prompt: data });
                })
                .catch(error => {
                    console.log(error);
                    this.setState({ prompt: "How was your day? How are you feeling?" });
                });
        } else {
            this.setState({ prompt: prompt });
        }
    }
    setPrompt = () => {
        const prompt = document.getElementById("setPrompt").value;
        this.setState({ prompt: prompt });
        localStorage.setItem("prompt", prompt);
    };
    handleChange = event => {
        this.setState({ currentEntry: event.target.value });
    };
    writeJournalEntry = () => {
        const entry = document.getElementById("journalInput").value;
        const entryObject = { timeStamp: new Date().toString(), actualText: entry };
        const newJournalEntries = [entryObject, ...this.state.journalEntries];
        this.setState({ currentEntry: "", journalEntries: newJournalEntries });
        localStorage.setItem("journalEntries", JSON.stringify(newJournalEntries));
    };
    render() {
        return (
            <div>
                <label>
                    Set your prompt here. <input type="text" id="setPrompt" />
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
                    <li>
                        <p>{this.state.currentEntry}</p>
                    </li>

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
