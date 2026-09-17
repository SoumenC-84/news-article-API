import { useState } from 'react'
import heroImg from './assets/hero.png'
import reactLogo from './assets/react.svg'
import viteLogo from './assets/vite.svg'
import './App.css'

function App() {
 const [article, setArticle] = useState("");
 const [summary, setSummary] = useState("");
 const [loading, setLoading] = useState(false);

 const summarizeArticle = async () => {

  setLoading(true);
  setSummary("");

  try {

    const response = await fetch(
      "http://localhost:5211/api/Chat",
      {
        method: "POST",

        headers: {
          "Content-Type": "application/json"
        },

        body: JSON.stringify({
          message: article
        })
      }
    );

    if (!response.ok) {
      throw new Error("API request failed");
    }

    const data = await response.json();

    setSummary(data.summary);

  }
  catch (error) {

    console.error(error);

    setSummary("Unable to generate summary.");

  }
  finally {

    setLoading(false);

  }
};

  return (
<div>
    <h1>News Article Summarizer</h1>
    <textarea
        placeholder="Paste your news article here..."
        value={article}
        onChange={(event) => setArticle(event.target.value)}
        rows={15}
        cols={80}
      />

      <br />
      <br />

      <button onClick={(event)=>summarizeArticle()}>
        Summarize Article
      </button>

      <h2>Summary</h2>

      {loading && <p>Generating summary...</p>}

      {!loading && <p>{summary}</p>}

    </div>

  )

}

export default App
