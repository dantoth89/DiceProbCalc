import React from 'react'
import { useState } from 'react';
import './CalculatorForm.css';
import { FormControl, InputLabel, MenuItem, Select, Input, Button, TextField } from '@mui/material';


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
                    <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                    <InputLabel id="input-label" htmlFor="number-of-attacks" shrink>Number of attacks:</InputLabel>
                    <TextField
                        id="input-field"
                        label='_______________'                       
                        variant="outlined"
                        size="small"
                        type="number" 
                        value={numberOfAttacks}
                        onChange={(e) => setNumberOfAttacks(Number(e.target.value))}
                    />
                    </FormControl>
                        <br></br>

                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Target number for attacks (2 - 6):</InputLabel>
                        <Select 
                            labelId="numberDropdown-label"
                            id="input-field"
                            value={targetRollToHit}
                            onChange={(e) => setTargetRollToHit(parseInt(e.target.value))}
                            label="Target number for attacks (2 - 6)"
                        >
                            {[2, 3, 4, 5, 6].map((number) => (
                            <MenuItem key={number} value={number}>
                                {number}+
                            </MenuItem>
                            ))}
                        </Select>
                        </FormControl>
                        <br></br>
                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Reroll</InputLabel>
                        <Select 
                            labelId="rerollDropdown-label"
                            id="input-field"
                            value={hitReRoll}
                            onChange={(e) => {
                                const selectedValue = parseInt(e.target.value);
                                setHitReRoll(selectedValue);
                                if (selectedValue === 1) {
                                    setHitToReRoll(1);
                                } else {
                                    setHitToReRoll(0);
                                }
                            }}     
                            label="Reroll"
                        >
                            <MenuItem key={0} value={0}>
                            No reroll
                            </MenuItem>
                            <MenuItem key={1} value={1}>
                            To hit 1
                            </MenuItem>
                            <MenuItem key={2} value={2}>
                            All failed
                            </MenuItem>
                        </Select>
                        </FormControl>                       
                        <br></br>

                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Hit on six event?:</InputLabel>
                        <Select 
                            labelId="hitOnSixEvent-label"
                            id="input-field"
                            value={hitOnSixEvent}
                            onChange={(e) => setHitOnSixEvent(parseInt(e.target.value))}
                            label="Hit on six event?"
                        >
                            <MenuItem key={0} value={0}>
                            No event
                            </MenuItem>
                            <MenuItem key={1} value={1}>
                            Plus one hit
                            </MenuItem>
                            <MenuItem key={2} value={2}>
                            Plus two hit
                            </MenuItem>
                            <MenuItem key={3} value={3}>
                            Auto wound
                            </MenuItem>
                            <MenuItem key={4} value={4}>
                            Additional mortal wound
                            </MenuItem>
                            <MenuItem key={5} value={5}>
                            Damage as mortal wound
                            </MenuItem>
                        </Select>
                        </FormControl>
                        <br></br>
                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Hit modifier (-1 / +1):</InputLabel>
                        <Select 
                            labelId="hitMod-label"
                            id="input-field"
                            value={hitMod}
                            onChange={(e) => setHitMod(parseInt(e.target.value))}
                            label="Hit modifier (-1 / +1)"
                        >
                            <MenuItem key={0} value={0}>
                            No mod
                            </MenuItem>
                            <MenuItem key={1} value={1}>
                            + 1
                            </MenuItem>
                            <MenuItem key={2} value={-1}>
                            - 1
                            </MenuItem>
                        </Select>
                        </FormControl>
                        <br></br>
                    </div>
                </div>

                <div className='input-panel'>
                    <h2 className='input-header'>
                        Wound stats
                    </h2>
                    <div className='input-box'>

                    <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">To wound target number (2 - 6):</InputLabel>
                        <Select 
                            labelId="targetRollToWound-label"
                            id="input-field"
                            value={targetRollToWound}
                            onChange={(e) => setTargetRollToWound(parseInt(e.target.value))}
                            label="To wound target number (2 - 6)"
                        >
                            {[2, 3, 4, 5, 6].map((number) => (
                            <MenuItem key={number} value={number}>
                                {number}+
                            </MenuItem>
                            ))}
                        </Select>
                        </FormControl>
                        <br></br>
                            <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                            <InputLabel id="input-label">Reroll</InputLabel>
                            <Select 
                                    labelId="woundReRoll-label"
                                id="input-field"
                                value={woundReRoll}
                                onChange={(e) => {
                                    const selectedValue = parseInt(e.target.value);
                                    setWoundReRoll(selectedValue);
                                    if (selectedValue === 1) {
                                        setWoundToReRoll(1);
                                    } else {
                                        setWoundToReRoll(0);
                                    }}}
                                label="Reroll?"
                            >
                                <MenuItem key={0} value={0}>
                                No reroll
                                </MenuItem>
                                <MenuItem key={1} value={1}>
                                To wound 1
                                </MenuItem>
                                <MenuItem key={2} value={2}>
                                All failed
                                </MenuItem>
                            </Select>
                            </FormControl>
                        <br></br>

                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Wound on six event:</InputLabel>
                        <Select 
                            labelId="woundOnSixEvent-label"
                            id="input-field"
                            value={woundOnSixEvent}
                            onChange={(e) => setWoundOnSixEvent(parseInt(e.target.value))}
                            label="Wound on six event"
                        >
                            <MenuItem key={0} value={0}>
                            No event
                            </MenuItem>
                            <MenuItem key={1} value={1}>
                            Additional mortal wound
                            </MenuItem>
                            <MenuItem key={2} value={2}>
                            Damage as mortal wound
                            </MenuItem>
                        </Select>
                        </FormControl>
                        <br></br>

                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Wound modifier (-1/+1):</InputLabel>
                        <Select 
                            labelId="woundMod-label"
                            id="input-field"
                            value={woundMod}
                            onChange={(e) => setWoundMod(parseInt(e.target.value))}
                            label="Wound modifier (-1 / +1)"
                        >
                            <MenuItem key={0} value={0}>
                            No mod
                            </MenuItem>
                            <MenuItem key={1} value={1}>
                            + 1
                            </MenuItem>
                            <MenuItem key={2} value={-1}>
                            - 1
                            </MenuItem>
                        </Select>
                        </FormControl>
                        <br></br>

                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Penetration:</InputLabel>
                        <Select 
                            labelId="penetration-label"
                            id="input-field"
                            value={penetration}
                            onChange={(e) => setPenetration(parseInt(e.target.value))}
                            label="Penetration"
                        >
                            <MenuItem key={0} value={0}>
                            -
                            </MenuItem>
                            <MenuItem key={1} value={1}>
                            -1
                            </MenuItem>
                            <MenuItem key={2} value={2}>
                            -2
                            </MenuItem>
                            <MenuItem key={3} value={3}>
                            -3
                            </MenuItem>
                            <MenuItem key={4} value={4}>
                            -4
                            </MenuItem>
                            <MenuItem key={5} value={5}>
                            -5
                            </MenuItem>
                            <MenuItem key={6} value={6}>
                            -6
                            </MenuItem>
                        </Select>
                        </FormControl>
                        <br></br>

                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label" shrink>Damage: </InputLabel>
                        <TextField
                            id="input-field"
                            label='_______' 
                            size="small"
                            type="number"        
                            value={damage}
                            onChange={(e) => setDamage(Number(e.target.value))}
                        />
                        </FormControl>
                        <br></br>
                    </div>
                </div>

                <div className='input-panel'>
                    <h2 className='input-header'>
                        Save stats
                    </h2>
                    <div className='input-box'>

                    <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                    <InputLabel id="input-label">Save:</InputLabel>
                    <Select 
                        class="dropdown-content"
                        labelId="save-label"
                        id="input-field"
                        value={save}
                        onChange={(e) => setSave(parseInt(e.target.value))}
                        label="Save"
                    >
                        {[2, 3, 4, 5, 6, 7].map((number) => (
                        <MenuItem key={number} value={number}>
                            {number}+
                        </MenuItem>
                        ))}
                    </Select>
                    </FormControl>
                        <br></br>

                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Save Modifier:</InputLabel>
                        <Select 
                            labelId="saveMod-label"
                            id="input-field"
                            value={saveMod}
                            onChange={(e) => setSaveMod(parseInt(e.target.value))}
                            label="Save Modifier"
                        >
                            <MenuItem key={0} value={0}>
                            No mod
                            </MenuItem>
                            <MenuItem key={1} value={1}>
                            + 1
                            </MenuItem>
                            <MenuItem key={2} value={-1}>
                            - 1
                            </MenuItem>
                        </Select>
                        </FormControl>
                        <br></br>

                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Is there a cover?:</InputLabel>
                        <Select 
                            labelId="cover-label"
                            id="input-field"
                            value={cover}
                            onChange={(e) => setCover(parseInt(e.target.value))}
                            label="Is there a cover?"
                        >
                            <MenuItem key={0} value={0}>
                            No
                            </MenuItem>
                            <MenuItem key={1} value={1}>
                            Yes
                            </MenuItem>
                        </Select>
                        </FormControl>
                        <br></br>

                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Reroll</InputLabel>
                        <Select 
                            labelId="saveReRoll-label"
                            id="input-field"
                            value={saveReRoll}
                            onChange={(e) => {
                                const selectedValue = parseInt(e.target.value);
                                setSaveReRoll(selectedValue);
                                if (selectedValue === 1) {
                                    setSaveToReRoll(1);
                                } else {
                                    setSaveToReRoll(0);
                                }
                            }}     
                            label="Reroll"
                        >
                            <MenuItem key={0} value={0}>
                            No reroll
                            </MenuItem>
                            <MenuItem key={1} value={1}>
                            To save 1
                            </MenuItem>
                            <MenuItem key={2} value={2}>
                            All failed
                            </MenuItem>
                        </Select>
                        </FormControl>
                        <br></br>

                        <FormControl sx={{ m: 1, minWidth: 220 }} size="small">
                        <InputLabel id="input-label">Feel No Pain:</InputLabel>
                        <Select 
                            labelId="feelNoPain-label"
                            id="input-field"
                            value={feelNoPain}
                            onChange={(e) => setFeelNoPain(parseInt(e.target.value))}
                            label="Feel No Pain"
                        >
                            <MenuItem key={0} value={0}>
                            No
                            </MenuItem>
                            <MenuItem key={2} value={2}>
                            2+
                            </MenuItem>
                            <MenuItem key={3} value={3}>
                            3+
                            </MenuItem>
                            <MenuItem key={4} value={4}>
                            4+
                            </MenuItem>
                            <MenuItem key={5} value={5}>
                            5+
                            </MenuItem>
                            <MenuItem key={6} value={6}>
                            6+
                            </MenuItem>
                        </Select>
                        </FormControl>
                        <br></br>
                    </div>
                </div>
            </div>
            <Button variant="contained" type="submit" className='submit-button'>Calculate</Button>
        </form>
        <Button className='final'>
            {parseInt(final) <= 0 ? "Set valid stats" : `Expected: [ ${final} ]`}
        </Button>
        <Button variant="contained" className='clear-button' onClick={clearStats}>Reset</Button>
    </div>
    );
};

