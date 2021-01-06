# YN.XFCounterLabelControl

<p>Animated Counter Control for Xamarin.Forms(Android and iOS) with some awesome features that will make your users more satisfied when they look into their numbers.</p>

## Setup
* Available on NuGet: https://www.nuget.org/packages/YN.XFCounterLabelControl [![NuGet](https://img.shields.io/nuget/v/YN.XFCounterLabelControl.svg?label=NuGet)](https://www.nuget.org/packages/YN.XFCounterLabelControl/)

## Preview

### Android
<img src="https://github.com/Yagon2395/YN.XFCounterLabelControl/blob/master/Images/cenarioAndroid.gif" width="300"/>

### iOS
<img src="https://github.com/Yagon2395/YN.XFCounterLabelControl/blob/master/Images/cenarioiOS.gif" width="300"/>


## How to Use
```XAML
<xfcounterlabelcontrol:CounterLabelControl
  AnimationDuration="1000"
  CounterType="Integer"
  StartValue="0"
  TargetValue="0"
  TextColor="DodgerBlue"/>
```

## Features
<table style="width:100%">
  <tr>
    <th>Properties</th>
    <th>Type</th>
    <th>Description</th>
  </tr>
  <tr>
    <td>StartValue</td>
    <td>int</td>
    <td>Start value for the animation.</td>
  </tr>
  <tr>
    <td>TargetValue</td>
    <td>int</td>
    <td>The target value to stop the animation.</td>
  </tr>
  <tr>
    <td>AnimationDuration</td>
    <td>uint</td>
    <td>The duration of the animation.</td>
  </tr>
  <tr>
    <td>CounterType</td>
    <td>CounterTypeEnum</td>
    <td>The type of data to display, possible values: int, double and Currency</td>
  </tr>
</table>

### Contact
<div>
  Any questions, issues or improvements feel free to contact me.
  <br>
  yagon2395@gmail.com
</div>

### License
GPL-3.0-or-later
