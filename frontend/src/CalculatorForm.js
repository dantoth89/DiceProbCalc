import React from 'react'
import { useState } from 'react';
import './CalculatorForm.css';

export default function CalculatorForm() {
    const [numberOfAttacks, setNumberOfAttacks] = useState(24);
    const [targetRollToHit, setTargetRollToHit] = useState(4);
    const [hitReRoll, setHitReRoll] = useState(0);
    const [hitToReRoll, setHitToReRoll] = useState(0);
    const [hitOnSixEvent, setHitOnSixEvent] = useState(0);
    const [hitMod, setHitMod] = useState(0);

    const [targetRollToWound, setTargetRollToWound] = useState(4);
    const [woundReRoll, setWoundReRoll] = useState(0);
    const [woundToReRoll, setWoundToReRoll] = useState(0);
    const [woundOnSixEvent, setWoundOnSixEvent] = useState(0);
    const [woundMod, setWoundMod] = useState(0);
    const [penetration, setPenetration] = useState(0);
    const [damage, setDamage] = useState(1);

    const [save, setSave] = useState(4);
    const [saveMod, setSaveMod] = useState(0);
    const [cover, setCover] = useState(0);
    const [saveReRoll, setSaveReRoll] = useState(0);
    const [saveToReRoll, setSaveToReRoll] = useState(0);
    const [feelNoPain, setFeelNoPain] = useState(0);

    const [final, setFinal] = useState(0);

    const clearStats = () => {
        setNumberOfAttacks(24);
        setTargetRollToHit(4);
        setHitReRoll(0);
        setHitToReRoll(0);
        setHitOnSixEvent(0);
        setHitMod(0);

        setTargetRollToWound(4);
        setWoundReRoll(0);
        setWoundToReRoll(0);
        setWoundOnSixEvent(0);
        setWoundMod(0);
        setPenetration(0);
        setDamage(1);

        setSave(4);
        setSaveMod(0);
        setCover(0);
        setSaveReRoll(0);
        setSaveToReRoll(0);
        setFeelNoPain(0);

        setFinal(-1);
    }

    const handleSubmit = (e) => {
        e.preventDefault();

        const formData = {
            numberOfAttacks,
            targetRollToHit,
            hitReRoll,
            hitToReRoll,
            hitOnSixEvent,
            hitMod,
            targetRollToWound,
            woundReRoll,
            woundToReRoll,
            woundOnSixEvent,
            woundMod,
            penetration,
            damage,
            save,
            saveMod,
            cover,
            saveReRoll,
            saveToReRoll,
            feelNoPain,
        };

        fetch('http://localhost:5090/body', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(formData),
        })
            .then((response) => response.text())
            .then((response) => setFinal(response))

    };


    return (<div className='all-panel'>
        <form onSubmit={handleSubmit}>
            <div className='all-input-panel'>
                <div className='input-panel'>
                    <h2 className='input-header'>
                        Attack stats
                    </h2>
                    <div className='input-box'>
                        <label className='input-field'>
                            Number of attacks:
                            <input type="number" value={numberOfAttacks} onChange={(e) => setNumberOfAttacks(Number(e.target.value))} />
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Target number for attacks (2 - 6):
                            <select id="numberDropdown" value={targetRollToHit} onChange={(e) => setTargetRollToHit(parseInt(e.target.value))}>
                                {[2, 3, 4, 5, 6].map((number) => (
                                    <option key={number} value={number}>
                                        {number}+
                                    </option>
                                ))}
                            </select>

                        </label>
                        <br></br>
                        <label className='input-field'>
                            Reroll?
                            <select value={hitReRoll} onChange={(e) => {
                                const selectedValue = parseInt(e.target.value);
                                setHitReRoll(selectedValue);
                                if (selectedValue === 1) {
                                    setHitToReRoll(1);
                                } else {
                                    setHitToReRoll(0);
                                }
                            }}>
                                <option key={0} value={0}>
                                    No reroll
                                </option>
                                <option key={1} value={1}>
                                    To hit 1
                                </option>
                                <option key={2} value={2}>
                                    All failed
                                </option>
                            </select>
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Hit on six event?:
                            <select id="numberDropdown" value={hitOnSixEvent} onChange={(e) => setHitOnSixEvent(parseInt(e.target.value))}>
                                {<>
                                    <option key={0} value={0}>
                                        No event
                                    </option>
                                    <option key={1} value={1}>
                                        Plus one hit
                                    </option>
                                    <option key={2} value={2}>
                                        Plus two hit
                                    </option>
                                    <option key={3} value={3}>
                                        Auto wound
                                    </option>
                                    <option key={4} value={4}>
                                        Additional mortal wound
                                    </option>
                                    <option key={5} value={5}>
                                        Damage as mortal wound
                                    </option>
                                </>
                                }
                            </select>
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Hit modifier (-1 / +1):
                            <select id="numberDropdown" value={hitMod} onChange={(e) => setHitMod(parseInt(e.target.value))}>
                                {<>
                                    <option key={0} value={0}>
                                        No mod
                                    </option>
                                    <option key={1} value={1}>
                                        + 1
                                    </option>
                                    <option key={2} value={-1}>
                                        - 1
                                    </option>
                                </>
                                }
                            </select>
                        </label>
                        <br></br>
                    </div>
                </div>

                <div className='input-panel'>
                    <h2 className='input-header'>
                        Wound stats
                    </h2>
                    <div className='input-box'>

                        <label className='input-field'>
                            To wound target number(2 - 6):
                            <select id="numberDropdown" value={targetRollToWound} onChange={(e) => setTargetRollToWound(parseInt(e.target.value))}>
                                {[2, 3, 4, 5, 6].map((number) => (
                                    <option key={number} value={number}>
                                        {number}+
                                    </option>
                                ))}
                            </select>
                        </label>
                        <br></br>


                        <label className='input-field'>
                            Reroll?
                            <select value={woundReRoll} onChange={(e) => {
                                const selectedValue = parseInt(e.target.value);
                                setWoundReRoll(selectedValue);
                                if (selectedValue === 1) {
                                    setWoundToReRoll(1);
                                } else {
                                    setWoundToReRoll(0);
                                }
                            }}>
                                <option key={0} value={0}>
                                    No reroll
                                </option>
                                <option key={1} value={1}>
                                    To wound 1
                                </option>
                                <option key={2} value={2}>
                                    All failed
                                </option>
                            </select>
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Wound on six event:
                            <select id="numberDropdown" value={woundOnSixEvent} onChange={(e) => setWoundOnSixEvent(parseInt(e.target.value))}>
                                {<>
                                    <option key={0} value={0}>
                                        No event
                                    </option>                                   
                                    <option key={1} value={1}>
                                        Additional mortal wound
                                    </option>
                                    <option key={2} value={1}>
                                        Damage as mortal wound
                                    </option>
                                </>
                                }
                            </select>
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Wound modifier (-1/+1):
                            <select id="numberDropdown" value={woundMod} onChange={(e) => setWoundMod(parseInt(e.target.value))}>
                                {<>
                                    <option key={0} value={0}>
                                        No mod
                                    </option>
                                    <option key={1} value={1}>
                                        + 1
                                    </option>
                                    <option key={2} value={-1}>
                                        - 1
                                    </option>
                                </>
                                }
                            </select>
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Penetration:
                            <select id="numberDropdown" value={penetration} onChange={(e) => setPenetration(parseInt(e.target.value))}>
                                <option key={0} value={0}>
                                    -
                                </option>
                                <option key={1} value={1}>
                                    -1
                                </option>
                                <option key={2} value={2}>
                                    -2
                                </option>
                                <option key={3} value={3}>
                                    -3
                                </option>
                                <option key={4} value={4}>
                                    -4
                                </option>
                                <option key={5} value={5}>
                                    -5
                                </option>
                                <option key={6} value={6}>
                                    -6
                                </option>
                            </select>
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Damage:
                            <input type="number" value={damage} onChange={(e) => setDamage(Number(e.target.value))} />
                        </label>
                        <br></br>
                    </div>
                </div>

                <div className='input-panel'>
                    <h2 className='input-header'>
                        Save stats
                    </h2>
                    <div className='input-box'>

                        <label className='input-field'>
                            Save:
                            <select id="numberDropdown" value={save} onChange={(e) => setSave(parseInt(e.target.value))}>
                                {[2, 3, 4, 5, 6, 7].map((number) => (
                                    <option key={number} value={number}>
                                        {number}+
                                    </option>
                                ))}
                            </select>
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Save Modifier:
                            <select id="numberDropdown" value={saveMod} onChange={(e) => setSaveMod(parseInt(e.target.value))}>
                                {<>
                                    <option key={0} value={0}>
                                        No mod
                                    </option>
                                    <option key={1} value={1}>
                                        + 1
                                    </option>
                                    <option key={2} value={-1}>
                                        - 1
                                    </option>
                                </>
                                }
                            </select>
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Is there a cover?:
                            <select id="numberDropdown" value={cover} onChange={(e) => setCover(parseInt(e.target.value))}>
                                {<>
                                    <option key={0} value={0}>
                                        No
                                    </option>
                                    <option key={1} value={1}>
                                        Yes
                                    </option>
                                </>
                                }
                            </select>
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Reroll?
                            <select value={saveReRoll} onChange={(e) => {
                                const selectedValue = parseInt(e.target.value);
                                setSaveReRoll(selectedValue);
                                if (selectedValue === 1) {
                                    setSaveToReRoll(1);
                                } else {
                                    setSaveToReRoll(0);
                                }
                            }}>
                                <option key={0} value={0}>
                                    No reroll
                                </option>
                                <option key={1} value={1}>
                                    To save 1
                                </option>
                                <option key={2} value={2}>
                                    All failed
                                </option>
                            </select>
                        </label>
                        <br></br>

                        <label className='input-field'>
                            Feel No Pain:
                            <select id="numberDropdown" value={feelNoPain} onChange={(e) => setFeelNoPain(parseInt(e.target.value))}>
                                <option key={0} value={0}>
                                    No
                                </option>
                                <option key={2} value={2}>
                                    2+
                                </option>
                                <option key={3} value={3}>
                                    3+
                                </option>
                                <option key={4} value={4}>
                                    4+
                                </option>
                                <option key={5} value={5}>
                                    5+
                                </option>
                                <option key={6} value={6}>
                                    6+
                                </option>
                            </select>
                        </label>
                        <br></br>
                    </div>
                </div>
            </div>
            <button type="submit" className='submit-button'>Calculate</button>
        </form>
        <div className='final'>
            {parseInt(final) <= 0 ? "Set valid stats" : `Expected: [ ${final} ] wound`}
        </div>
        <button className='clear-button' onClick={clearStats}>Reset</button>
    </div>
    );
};

