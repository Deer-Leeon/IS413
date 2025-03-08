import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import Heading from './components/Heading';
import TeamList from './components/TeamList';
import './App.css'


function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div className="app">
        <Heading />
        <TeamList />
      </div>
    </>
  )
}

export default App
