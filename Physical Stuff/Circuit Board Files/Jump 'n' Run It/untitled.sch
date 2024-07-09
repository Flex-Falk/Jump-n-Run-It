<?xml version="1.0" encoding="utf-8"?>
<!DOCTYPE eagle SYSTEM "eagle.dtd">
<eagle version="8.0.0">
<drawing>
<settings>
<setting alwaysvectorfont="no"/>
<setting verticaltext="up"/>
</settings>
<grid distance="0.1" unitdist="inch" unit="inch" style="lines" multiple="1" display="no" altdistance="0.01" altunitdist="inch" altunit="inch"/>
<layers>
<layer number="1" name="Top" color="4" fill="1" visible="no" active="no"/>
<layer number="2" name="Route2" color="1" fill="3" visible="no" active="no"/>
<layer number="3" name="Route3" color="4" fill="3" visible="no" active="no"/>
<layer number="4" name="Route4" color="1" fill="4" visible="no" active="no"/>
<layer number="5" name="Route5" color="4" fill="4" visible="no" active="no"/>
<layer number="6" name="Route6" color="1" fill="8" visible="no" active="no"/>
<layer number="7" name="Route7" color="4" fill="8" visible="no" active="no"/>
<layer number="8" name="Route8" color="1" fill="2" visible="no" active="no"/>
<layer number="9" name="Route9" color="4" fill="2" visible="no" active="no"/>
<layer number="10" name="Route10" color="1" fill="7" visible="no" active="no"/>
<layer number="11" name="Route11" color="4" fill="7" visible="no" active="no"/>
<layer number="12" name="Route12" color="1" fill="5" visible="no" active="no"/>
<layer number="13" name="Route13" color="4" fill="5" visible="no" active="no"/>
<layer number="14" name="Route14" color="1" fill="6" visible="no" active="no"/>
<layer number="15" name="Route15" color="4" fill="6" visible="no" active="no"/>
<layer number="16" name="Bottom" color="1" fill="1" visible="no" active="no"/>
<layer number="17" name="Pads" color="2" fill="1" visible="no" active="no"/>
<layer number="18" name="Vias" color="2" fill="1" visible="no" active="no"/>
<layer number="19" name="Unrouted" color="6" fill="1" visible="no" active="no"/>
<layer number="20" name="Dimension" color="24" fill="1" visible="no" active="no"/>
<layer number="21" name="tPlace" color="7" fill="1" visible="no" active="no"/>
<layer number="22" name="bPlace" color="7" fill="1" visible="no" active="no"/>
<layer number="23" name="tOrigins" color="15" fill="1" visible="no" active="no"/>
<layer number="24" name="bOrigins" color="15" fill="1" visible="no" active="no"/>
<layer number="25" name="tNames" color="7" fill="1" visible="no" active="no"/>
<layer number="26" name="bNames" color="7" fill="1" visible="no" active="no"/>
<layer number="27" name="tValues" color="7" fill="1" visible="no" active="no"/>
<layer number="28" name="bValues" color="7" fill="1" visible="no" active="no"/>
<layer number="29" name="tStop" color="7" fill="3" visible="no" active="no"/>
<layer number="30" name="bStop" color="7" fill="6" visible="no" active="no"/>
<layer number="31" name="tCream" color="7" fill="4" visible="no" active="no"/>
<layer number="32" name="bCream" color="7" fill="5" visible="no" active="no"/>
<layer number="33" name="tFinish" color="6" fill="3" visible="no" active="no"/>
<layer number="34" name="bFinish" color="6" fill="6" visible="no" active="no"/>
<layer number="35" name="tGlue" color="7" fill="4" visible="no" active="no"/>
<layer number="36" name="bGlue" color="7" fill="5" visible="no" active="no"/>
<layer number="37" name="tTest" color="7" fill="1" visible="no" active="no"/>
<layer number="38" name="bTest" color="7" fill="1" visible="no" active="no"/>
<layer number="39" name="tKeepout" color="4" fill="11" visible="no" active="no"/>
<layer number="40" name="bKeepout" color="1" fill="11" visible="no" active="no"/>
<layer number="41" name="tRestrict" color="4" fill="10" visible="no" active="no"/>
<layer number="42" name="bRestrict" color="1" fill="10" visible="no" active="no"/>
<layer number="43" name="vRestrict" color="2" fill="10" visible="no" active="no"/>
<layer number="44" name="Drills" color="7" fill="1" visible="no" active="no"/>
<layer number="45" name="Holes" color="7" fill="1" visible="no" active="no"/>
<layer number="46" name="Milling" color="3" fill="1" visible="no" active="no"/>
<layer number="47" name="Measures" color="7" fill="1" visible="no" active="no"/>
<layer number="48" name="Document" color="7" fill="1" visible="no" active="no"/>
<layer number="49" name="Reference" color="7" fill="1" visible="no" active="no"/>
<layer number="51" name="tDocu" color="7" fill="1" visible="no" active="no"/>
<layer number="52" name="bDocu" color="7" fill="1" visible="no" active="no"/>
<layer number="88" name="SimResults" color="9" fill="1" visible="yes" active="yes"/>
<layer number="89" name="SimProbes" color="9" fill="1" visible="yes" active="yes"/>
<layer number="90" name="Modules" color="5" fill="1" visible="yes" active="yes"/>
<layer number="91" name="Nets" color="2" fill="1" visible="yes" active="yes"/>
<layer number="92" name="Busses" color="1" fill="1" visible="yes" active="yes"/>
<layer number="93" name="Pins" color="2" fill="1" visible="no" active="yes"/>
<layer number="94" name="Symbols" color="4" fill="1" visible="yes" active="yes"/>
<layer number="95" name="Names" color="7" fill="1" visible="yes" active="yes"/>
<layer number="96" name="Values" color="7" fill="1" visible="yes" active="yes"/>
<layer number="97" name="Info" color="7" fill="1" visible="yes" active="yes"/>
<layer number="98" name="Guide" color="6" fill="1" visible="yes" active="yes"/>
</layers>
<schematic xreflabel="%F%N/%S.%C%R" xrefpart="/%S.%C%R">
<libraries>
<library name="ESP32-DEVKITC">
<packages>
<package name="ESP32-DEVKITC">
<pad name="1" x="-22.87" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="2" x="-20.33" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="3" x="-17.79" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="4" x="-15.25" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="5" x="-12.71" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="6" x="-10.17" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="7" x="-7.63" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="8" x="-5.09" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="9" x="-2.55" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="10" x="-0.01" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="11" x="2.53" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="12" x="5.07" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="13" x="7.61" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="14" x="10.15" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="15" x="12.69" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="16" x="15.23" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="17" x="17.77" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="18" x="20.31" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="19" x="22.85" y="-13.96" drill="1" diameter="1.9304"/>
<pad name="38" x="-22.87" y="11.23" drill="1" diameter="1.9304"/>
<pad name="37" x="-20.33" y="11.23" drill="1" diameter="1.9304"/>
<pad name="36" x="-17.79" y="11.23" drill="1" diameter="1.9304"/>
<pad name="35" x="-15.25" y="11.23" drill="1" diameter="1.9304"/>
<pad name="34" x="-12.71" y="11.23" drill="1" diameter="1.9304"/>
<pad name="33" x="-10.17" y="11.23" drill="1" diameter="1.9304"/>
<pad name="32" x="-7.63" y="11.23" drill="1" diameter="1.9304"/>
<pad name="31" x="-5.09" y="11.23" drill="1" diameter="1.9304"/>
<pad name="30" x="-2.55" y="11.23" drill="1" diameter="1.9304"/>
<pad name="29" x="-0.01" y="11.23" drill="1" diameter="1.9304"/>
<pad name="28" x="2.53" y="11.23" drill="1" diameter="1.9304"/>
<pad name="27" x="5.07" y="11.23" drill="1" diameter="1.9304"/>
<pad name="26" x="7.61" y="11.23" drill="1" diameter="1.9304"/>
<pad name="25" x="10.15" y="11.23" drill="1" diameter="1.9304"/>
<pad name="24" x="12.69" y="11.23" drill="1" diameter="1.9304"/>
<pad name="23" x="15.23" y="11.23" drill="1" diameter="1.9304"/>
<pad name="22" x="17.77" y="11.23" drill="1" diameter="1.9304"/>
<pad name="21" x="20.31" y="11.23" drill="1" diameter="1.9304"/>
<pad name="20" x="22.85" y="11.23" drill="1" diameter="1.9304"/>
<wire x1="-22.85" y1="5.86" x2="-22.85" y2="2.02" width="0.4064" layer="21"/>
<wire x1="-22.85" y1="2.02" x2="-17.73" y2="2.02" width="0.4064" layer="21"/>
<wire x1="-17.73" y1="2.02" x2="-17.73" y2="0.74" width="0.4064" layer="21"/>
<wire x1="-17.73" y1="0.74" x2="-22.85" y2="0.74" width="0.4064" layer="21"/>
<wire x1="-22.85" y1="0.74" x2="-22.85" y2="-0.54" width="0.4064" layer="21"/>
<wire x1="-22.85" y1="-0.54" x2="-17.73" y2="-0.54" width="0.4064" layer="21"/>
<wire x1="-17.73" y1="-0.54" x2="-17.73" y2="-1.82" width="0.4064" layer="21"/>
<wire x1="-17.73" y1="-1.82" x2="-22.85" y2="-1.82" width="0.4064" layer="21"/>
<wire x1="-22.85" y1="-1.82" x2="-22.85" y2="-3.1" width="0.4064" layer="21"/>
<wire x1="-22.85" y1="-3.1" x2="-17.73" y2="-3.1" width="0.4064" layer="21"/>
<wire x1="-17.73" y1="-3.1" x2="-17.73" y2="-4.38" width="0.4064" layer="21"/>
<wire x1="-17.73" y1="-4.38" x2="-22.85" y2="-4.38" width="0.4064" layer="21"/>
<wire x1="-22.85" y1="-4.38" x2="-22.85" y2="-5.66" width="0.4064" layer="21"/>
<wire x1="-22.85" y1="-5.66" x2="-15.17" y2="-5.66" width="0.4064" layer="21"/>
<wire x1="-15.17" y1="-5.66" x2="-15.17" y2="0.74" width="0.4064" layer="21"/>
<text x="-22.21" y="-12.04" size="1.016" layer="21" rot="R90">3V3</text>
<text x="-19.67" y="-12.04" size="1.016" layer="21" rot="R90">EN</text>
<text x="-17.13" y="-12.04" size="1.016" layer="21" rot="R90">SVP</text>
<text x="-14.59" y="-12.04" size="1.016" layer="21" rot="R90">SVN</text>
<text x="-12.05" y="-12.04" size="1.016" layer="21" rot="R90">IO34</text>
<text x="-9.51" y="-12.04" size="1.016" layer="21" rot="R90">IO35</text>
<text x="-6.97" y="-12.04" size="1.016" layer="21" rot="R90">IO32</text>
<text x="-4.43" y="-12.04" size="1.016" layer="21" rot="R90">IO33</text>
<text x="-1.89" y="-12.04" size="1.016" layer="21" rot="R90">IO25</text>
<text x="0.65" y="-12.04" size="1.016" layer="21" rot="R90">IO26</text>
<text x="3.19" y="-12.04" size="1.016" layer="21" rot="R90">IO27</text>
<text x="5.73" y="-12.04" size="1.016" layer="21" rot="R90">IO14</text>
<text x="8.27" y="-12.04" size="1.016" layer="21" rot="R90">IO12</text>
<text x="10.81" y="-12.04" size="1.016" layer="21" rot="R90">GND</text>
<text x="13.35" y="-12.04" size="1.016" layer="21" rot="R90">IO13</text>
<text x="15.89" y="-12.04" size="1.016" layer="21" rot="R90">SD2</text>
<text x="18.43" y="-12.04" size="1.016" layer="21" rot="R90">SD3</text>
<text x="20.97" y="-12.04" size="1.016" layer="21" rot="R90">CMD</text>
<text x="23.51" y="-12.04" size="1.016" layer="21" rot="R90">5V</text>
<text x="-22.19" y="6.52" size="1.016" layer="21" rot="R90">GND</text>
<text x="-19.65" y="6.52" size="1.016" layer="21" rot="R90">IO23</text>
<text x="-17.11" y="6.52" size="1.016" layer="21" rot="R90">IO22</text>
<text x="-14.57" y="6.52" size="1.016" layer="21" rot="R90">TXD0</text>
<text x="-12.03" y="6.52" size="1.016" layer="21" rot="R90">RXD0</text>
<text x="-9.49" y="6.52" size="1.016" layer="21" rot="R90">IO21</text>
<text x="-6.95" y="6.52" size="1.016" layer="21" rot="R90">GND</text>
<text x="-4.41" y="6.52" size="1.016" layer="21" rot="R90">IO19</text>
<text x="-1.87" y="6.52" size="1.016" layer="21" rot="R90">IO18</text>
<text x="0.67" y="6.52" size="1.016" layer="21" rot="R90">IO5</text>
<text x="3.21" y="6.52" size="1.016" layer="21" rot="R90">IO17</text>
<text x="5.75" y="6.52" size="1.016" layer="21" rot="R90">IO16</text>
<text x="8.29" y="6.52" size="1.016" layer="21" rot="R90">IO4</text>
<text x="10.83" y="6.52" size="1.016" layer="21" rot="R90">IO0</text>
<text x="13.37" y="6.52" size="1.016" layer="21" rot="R90">IO2</text>
<text x="15.91" y="6.52" size="1.016" layer="21" rot="R90">IO15</text>
<text x="18.45" y="6.52" size="1.016" layer="21" rot="R90">SD1</text>
<text x="20.99" y="6.52" size="1.016" layer="21" rot="R90">SD0</text>
<text x="23.53" y="6.52" size="1.016" layer="21" rot="R90">CLK</text>
<text x="-4.93" y="-1.18" size="1.9304" layer="21">ESP32-DevkitC</text>
<wire x1="-24.13" y1="12.7" x2="24.13" y2="12.7" width="0.254" layer="21"/>
<wire x1="24.13" y1="12.7" x2="24.13" y2="-15.24" width="0.254" layer="21"/>
<wire x1="24.13" y1="-15.24" x2="-24.13" y2="-15.24" width="0.254" layer="21"/>
<wire x1="-24.13" y1="-15.24" x2="-24.13" y2="12.7" width="0.254" layer="21"/>
<text x="-24.13" y="13.97" size="1.27" layer="21">&gt;NAME</text>
<text x="10.16" y="-15.24" size="1.27" layer="27">ESP32-DEVKITC</text>
</package>
</packages>
<symbols>
<symbol name="ESP32-DEVKITC">
<wire x1="-25.4" y1="-12.7" x2="-25.4" y2="12.7" width="0.254" layer="94"/>
<wire x1="-25.4" y1="12.7" x2="25.4" y2="12.7" width="0.254" layer="94"/>
<wire x1="25.4" y1="12.7" x2="25.4" y2="-12.7" width="0.254" layer="94"/>
<wire x1="25.4" y1="-12.7" x2="-25.4" y2="-12.7" width="0.254" layer="94"/>
<pin name="3V3" x="-22.86" y="-17.78" length="middle" rot="R90"/>
<pin name="EN" x="-20.32" y="-17.78" length="middle" rot="R90"/>
<pin name="SVP" x="-17.78" y="-17.78" length="middle" rot="R90"/>
<pin name="SVN" x="-15.24" y="-17.78" length="middle" rot="R90"/>
<pin name="IO34" x="-12.7" y="-17.78" length="middle" rot="R90"/>
<pin name="IO35" x="-10.16" y="-17.78" length="middle" rot="R90"/>
<pin name="IO32" x="-7.62" y="-17.78" length="middle" rot="R90"/>
<pin name="IO33" x="-5.08" y="-17.78" length="middle" rot="R90"/>
<pin name="IO25" x="-2.54" y="-17.78" length="middle" rot="R90"/>
<pin name="IO26" x="0" y="-17.78" length="middle" rot="R90"/>
<pin name="IO27" x="2.54" y="-17.78" length="middle" rot="R90"/>
<pin name="IO14" x="5.08" y="-17.78" length="middle" rot="R90"/>
<pin name="IO12" x="7.62" y="-17.78" length="middle" rot="R90"/>
<pin name="GND@14" x="10.16" y="-17.78" length="middle" rot="R90"/>
<pin name="IO13" x="12.7" y="-17.78" length="middle" rot="R90"/>
<pin name="SD2" x="15.24" y="-17.78" length="middle" rot="R90"/>
<pin name="SD3" x="17.78" y="-17.78" length="middle" rot="R90"/>
<pin name="CMD" x="20.32" y="-17.78" length="middle" rot="R90"/>
<pin name="5V" x="22.86" y="-17.78" length="middle" rot="R90"/>
<pin name="CLK" x="22.86" y="17.78" length="middle" rot="R270"/>
<pin name="SD0" x="20.32" y="17.78" length="middle" rot="R270"/>
<pin name="SD1" x="17.78" y="17.78" length="middle" rot="R270"/>
<pin name="IO15" x="15.24" y="17.78" length="middle" rot="R270"/>
<pin name="IO2" x="12.7" y="17.78" length="middle" rot="R270"/>
<pin name="IO0" x="10.16" y="17.78" length="middle" rot="R270"/>
<pin name="IO4" x="7.62" y="17.78" length="middle" rot="R270"/>
<pin name="IO16" x="5.08" y="17.78" length="middle" rot="R270"/>
<pin name="IO17" x="2.54" y="17.78" length="middle" rot="R270"/>
<pin name="IO5" x="0" y="17.78" length="middle" rot="R270"/>
<pin name="IO18" x="-2.54" y="17.78" length="middle" rot="R270"/>
<pin name="IO19" x="-5.08" y="17.78" length="middle" rot="R270"/>
<pin name="GND@32" x="-7.62" y="17.78" length="middle" rot="R270"/>
<pin name="IO21" x="-10.16" y="17.78" length="middle" rot="R270"/>
<pin name="RXD0" x="-12.7" y="17.78" length="middle" rot="R270"/>
<pin name="TXD0" x="-15.24" y="17.78" length="middle" rot="R270"/>
<pin name="IO22" x="-17.78" y="17.78" length="middle" rot="R270"/>
<pin name="IO23" x="-20.32" y="17.78" length="middle" rot="R270"/>
<pin name="GND@38" x="-22.86" y="17.78" length="middle" rot="R270"/>
<text x="-26.67" y="5.08" size="1.27" layer="95" rot="R90">&gt;NAME</text>
<text x="27.94" y="-12.7" size="1.27" layer="96" rot="R90">ESP32-DEVKITC</text>
</symbol>
</symbols>
<devicesets>
<deviceset name="ESP32DEVKITC">
<gates>
<gate name="G$1" symbol="ESP32-DEVKITC" x="0" y="0"/>
</gates>
<devices>
<device name="" package="ESP32-DEVKITC">
<connects>
<connect gate="G$1" pin="3V3" pad="1"/>
<connect gate="G$1" pin="5V" pad="19"/>
<connect gate="G$1" pin="CLK" pad="20"/>
<connect gate="G$1" pin="CMD" pad="18"/>
<connect gate="G$1" pin="EN" pad="2"/>
<connect gate="G$1" pin="GND@14" pad="14"/>
<connect gate="G$1" pin="GND@32" pad="32"/>
<connect gate="G$1" pin="GND@38" pad="38"/>
<connect gate="G$1" pin="IO0" pad="25"/>
<connect gate="G$1" pin="IO12" pad="13"/>
<connect gate="G$1" pin="IO13" pad="15"/>
<connect gate="G$1" pin="IO14" pad="12"/>
<connect gate="G$1" pin="IO15" pad="23"/>
<connect gate="G$1" pin="IO16" pad="27"/>
<connect gate="G$1" pin="IO17" pad="28"/>
<connect gate="G$1" pin="IO18" pad="30"/>
<connect gate="G$1" pin="IO19" pad="31"/>
<connect gate="G$1" pin="IO2" pad="24"/>
<connect gate="G$1" pin="IO21" pad="33"/>
<connect gate="G$1" pin="IO22" pad="36"/>
<connect gate="G$1" pin="IO23" pad="37"/>
<connect gate="G$1" pin="IO25" pad="9"/>
<connect gate="G$1" pin="IO26" pad="10"/>
<connect gate="G$1" pin="IO27" pad="11"/>
<connect gate="G$1" pin="IO32" pad="7"/>
<connect gate="G$1" pin="IO33" pad="8"/>
<connect gate="G$1" pin="IO34" pad="5"/>
<connect gate="G$1" pin="IO35" pad="6"/>
<connect gate="G$1" pin="IO4" pad="26"/>
<connect gate="G$1" pin="IO5" pad="29"/>
<connect gate="G$1" pin="RXD0" pad="34"/>
<connect gate="G$1" pin="SD0" pad="21"/>
<connect gate="G$1" pin="SD1" pad="22"/>
<connect gate="G$1" pin="SD2" pad="16"/>
<connect gate="G$1" pin="SD3" pad="17"/>
<connect gate="G$1" pin="SVN" pad="4"/>
<connect gate="G$1" pin="SVP" pad="3"/>
<connect gate="G$1" pin="TXD0" pad="35"/>
</connects>
<technologies>
<technology name=""/>
</technologies>
</device>
</devices>
</deviceset>
</devicesets>
</library>
<library name="pinhead">
<description>&lt;b&gt;Pin Header Connectors&lt;/b&gt;&lt;p&gt;
&lt;author&gt;Created by librarian@cadsoft.de&lt;/author&gt;</description>
<packages>
<package name="1X02">
<description>&lt;b&gt;PIN HEADER&lt;/b&gt;</description>
<wire x1="-1.905" y1="1.27" x2="-0.635" y2="1.27" width="0.1524" layer="21"/>
<wire x1="-0.635" y1="1.27" x2="0" y2="0.635" width="0.1524" layer="21"/>
<wire x1="0" y1="0.635" x2="0" y2="-0.635" width="0.1524" layer="21"/>
<wire x1="0" y1="-0.635" x2="-0.635" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="-2.54" y1="0.635" x2="-2.54" y2="-0.635" width="0.1524" layer="21"/>
<wire x1="-1.905" y1="1.27" x2="-2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="-2.54" y1="-0.635" x2="-1.905" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="-0.635" y1="-1.27" x2="-1.905" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="0" y1="0.635" x2="0.635" y2="1.27" width="0.1524" layer="21"/>
<wire x1="0.635" y1="1.27" x2="1.905" y2="1.27" width="0.1524" layer="21"/>
<wire x1="1.905" y1="1.27" x2="2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="2.54" y1="0.635" x2="2.54" y2="-0.635" width="0.1524" layer="21"/>
<wire x1="2.54" y1="-0.635" x2="1.905" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="1.905" y1="-1.27" x2="0.635" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="0.635" y1="-1.27" x2="0" y2="-0.635" width="0.1524" layer="21"/>
<pad name="1" x="-1.27" y="0" drill="1.016" shape="long" rot="R90"/>
<pad name="2" x="1.27" y="0" drill="1.016" shape="long" rot="R90"/>
<text x="-2.6162" y="1.8288" size="1.27" layer="25" ratio="10">&gt;NAME</text>
<text x="-2.54" y="-3.175" size="1.27" layer="27">&gt;VALUE</text>
<rectangle x1="-1.524" y1="-0.254" x2="-1.016" y2="0.254" layer="51"/>
<rectangle x1="1.016" y1="-0.254" x2="1.524" y2="0.254" layer="51"/>
</package>
<package name="1X02/90">
<description>&lt;b&gt;PIN HEADER&lt;/b&gt;</description>
<wire x1="-2.54" y1="-1.905" x2="0" y2="-1.905" width="0.1524" layer="21"/>
<wire x1="0" y1="-1.905" x2="0" y2="0.635" width="0.1524" layer="21"/>
<wire x1="0" y1="0.635" x2="-2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="-2.54" y1="0.635" x2="-2.54" y2="-1.905" width="0.1524" layer="21"/>
<wire x1="-1.27" y1="6.985" x2="-1.27" y2="1.27" width="0.762" layer="21"/>
<wire x1="0" y1="-1.905" x2="2.54" y2="-1.905" width="0.1524" layer="21"/>
<wire x1="2.54" y1="-1.905" x2="2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="2.54" y1="0.635" x2="0" y2="0.635" width="0.1524" layer="21"/>
<wire x1="1.27" y1="6.985" x2="1.27" y2="1.27" width="0.762" layer="21"/>
<pad name="1" x="-1.27" y="-3.81" drill="1.016" shape="long" rot="R90"/>
<pad name="2" x="1.27" y="-3.81" drill="1.016" shape="long" rot="R90"/>
<text x="-3.175" y="-3.81" size="1.27" layer="25" ratio="10" rot="R90">&gt;NAME</text>
<text x="4.445" y="-3.81" size="1.27" layer="27" rot="R90">&gt;VALUE</text>
<rectangle x1="-1.651" y1="0.635" x2="-0.889" y2="1.143" layer="21"/>
<rectangle x1="0.889" y1="0.635" x2="1.651" y2="1.143" layer="21"/>
<rectangle x1="-1.651" y1="-2.921" x2="-0.889" y2="-1.905" layer="21"/>
<rectangle x1="0.889" y1="-2.921" x2="1.651" y2="-1.905" layer="21"/>
</package>
<package name="1X04">
<description>&lt;b&gt;PIN HEADER&lt;/b&gt;</description>
<wire x1="0" y1="0.635" x2="0.635" y2="1.27" width="0.1524" layer="21"/>
<wire x1="0.635" y1="1.27" x2="1.905" y2="1.27" width="0.1524" layer="21"/>
<wire x1="1.905" y1="1.27" x2="2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="2.54" y1="0.635" x2="2.54" y2="-0.635" width="0.1524" layer="21"/>
<wire x1="2.54" y1="-0.635" x2="1.905" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="1.905" y1="-1.27" x2="0.635" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="0.635" y1="-1.27" x2="0" y2="-0.635" width="0.1524" layer="21"/>
<wire x1="-4.445" y1="1.27" x2="-3.175" y2="1.27" width="0.1524" layer="21"/>
<wire x1="-3.175" y1="1.27" x2="-2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="-2.54" y1="0.635" x2="-2.54" y2="-0.635" width="0.1524" layer="21"/>
<wire x1="-2.54" y1="-0.635" x2="-3.175" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="-2.54" y1="0.635" x2="-1.905" y2="1.27" width="0.1524" layer="21"/>
<wire x1="-1.905" y1="1.27" x2="-0.635" y2="1.27" width="0.1524" layer="21"/>
<wire x1="-0.635" y1="1.27" x2="0" y2="0.635" width="0.1524" layer="21"/>
<wire x1="0" y1="0.635" x2="0" y2="-0.635" width="0.1524" layer="21"/>
<wire x1="0" y1="-0.635" x2="-0.635" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="-0.635" y1="-1.27" x2="-1.905" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="-1.905" y1="-1.27" x2="-2.54" y2="-0.635" width="0.1524" layer="21"/>
<wire x1="-5.08" y1="0.635" x2="-5.08" y2="-0.635" width="0.1524" layer="21"/>
<wire x1="-4.445" y1="1.27" x2="-5.08" y2="0.635" width="0.1524" layer="21"/>
<wire x1="-5.08" y1="-0.635" x2="-4.445" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="-3.175" y1="-1.27" x2="-4.445" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="3.175" y1="1.27" x2="4.445" y2="1.27" width="0.1524" layer="21"/>
<wire x1="4.445" y1="1.27" x2="5.08" y2="0.635" width="0.1524" layer="21"/>
<wire x1="5.08" y1="0.635" x2="5.08" y2="-0.635" width="0.1524" layer="21"/>
<wire x1="5.08" y1="-0.635" x2="4.445" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="3.175" y1="1.27" x2="2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="2.54" y1="-0.635" x2="3.175" y2="-1.27" width="0.1524" layer="21"/>
<wire x1="4.445" y1="-1.27" x2="3.175" y2="-1.27" width="0.1524" layer="21"/>
<pad name="1" x="-3.81" y="0" drill="1.016" shape="long" rot="R90"/>
<pad name="2" x="-1.27" y="0" drill="1.016" shape="long" rot="R90"/>
<pad name="3" x="1.27" y="0" drill="1.016" shape="long" rot="R90"/>
<pad name="4" x="3.81" y="0" drill="1.016" shape="long" rot="R90"/>
<text x="-5.1562" y="1.8288" size="1.27" layer="25" ratio="10">&gt;NAME</text>
<text x="-5.08" y="-3.175" size="1.27" layer="27">&gt;VALUE</text>
<rectangle x1="1.016" y1="-0.254" x2="1.524" y2="0.254" layer="51"/>
<rectangle x1="-1.524" y1="-0.254" x2="-1.016" y2="0.254" layer="51"/>
<rectangle x1="-4.064" y1="-0.254" x2="-3.556" y2="0.254" layer="51"/>
<rectangle x1="3.556" y1="-0.254" x2="4.064" y2="0.254" layer="51"/>
</package>
<package name="1X04/90">
<description>&lt;b&gt;PIN HEADER&lt;/b&gt;</description>
<wire x1="-5.08" y1="-1.905" x2="-2.54" y2="-1.905" width="0.1524" layer="21"/>
<wire x1="-2.54" y1="-1.905" x2="-2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="-2.54" y1="0.635" x2="-5.08" y2="0.635" width="0.1524" layer="21"/>
<wire x1="-5.08" y1="0.635" x2="-5.08" y2="-1.905" width="0.1524" layer="21"/>
<wire x1="-3.81" y1="6.985" x2="-3.81" y2="1.27" width="0.762" layer="21"/>
<wire x1="-2.54" y1="-1.905" x2="0" y2="-1.905" width="0.1524" layer="21"/>
<wire x1="0" y1="-1.905" x2="0" y2="0.635" width="0.1524" layer="21"/>
<wire x1="0" y1="0.635" x2="-2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="-1.27" y1="6.985" x2="-1.27" y2="1.27" width="0.762" layer="21"/>
<wire x1="0" y1="-1.905" x2="2.54" y2="-1.905" width="0.1524" layer="21"/>
<wire x1="2.54" y1="-1.905" x2="2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="2.54" y1="0.635" x2="0" y2="0.635" width="0.1524" layer="21"/>
<wire x1="1.27" y1="6.985" x2="1.27" y2="1.27" width="0.762" layer="21"/>
<wire x1="2.54" y1="-1.905" x2="5.08" y2="-1.905" width="0.1524" layer="21"/>
<wire x1="5.08" y1="-1.905" x2="5.08" y2="0.635" width="0.1524" layer="21"/>
<wire x1="5.08" y1="0.635" x2="2.54" y2="0.635" width="0.1524" layer="21"/>
<wire x1="3.81" y1="6.985" x2="3.81" y2="1.27" width="0.762" layer="21"/>
<pad name="1" x="-3.81" y="-3.81" drill="1.016" shape="long" rot="R90"/>
<pad name="2" x="-1.27" y="-3.81" drill="1.016" shape="long" rot="R90"/>
<pad name="3" x="1.27" y="-3.81" drill="1.016" shape="long" rot="R90"/>
<pad name="4" x="3.81" y="-3.81" drill="1.016" shape="long" rot="R90"/>
<text x="-5.715" y="-3.81" size="1.27" layer="25" ratio="10" rot="R90">&gt;NAME</text>
<text x="6.985" y="-4.445" size="1.27" layer="27" rot="R90">&gt;VALUE</text>
<rectangle x1="-4.191" y1="0.635" x2="-3.429" y2="1.143" layer="21"/>
<rectangle x1="-1.651" y1="0.635" x2="-0.889" y2="1.143" layer="21"/>
<rectangle x1="0.889" y1="0.635" x2="1.651" y2="1.143" layer="21"/>
<rectangle x1="3.429" y1="0.635" x2="4.191" y2="1.143" layer="21"/>
<rectangle x1="-4.191" y1="-2.921" x2="-3.429" y2="-1.905" layer="21"/>
<rectangle x1="-1.651" y1="-2.921" x2="-0.889" y2="-1.905" layer="21"/>
<rectangle x1="0.889" y1="-2.921" x2="1.651" y2="-1.905" layer="21"/>
<rectangle x1="3.429" y1="-2.921" x2="4.191" y2="-1.905" layer="21"/>
</package>
</packages>
<symbols>
<symbol name="PINHD2">
<wire x1="-6.35" y1="-2.54" x2="1.27" y2="-2.54" width="0.4064" layer="94"/>
<wire x1="1.27" y1="-2.54" x2="1.27" y2="5.08" width="0.4064" layer="94"/>
<wire x1="1.27" y1="5.08" x2="-6.35" y2="5.08" width="0.4064" layer="94"/>
<wire x1="-6.35" y1="5.08" x2="-6.35" y2="-2.54" width="0.4064" layer="94"/>
<text x="-6.35" y="5.715" size="1.778" layer="95">&gt;NAME</text>
<text x="-6.35" y="-5.08" size="1.778" layer="96">&gt;VALUE</text>
<pin name="1" x="-2.54" y="2.54" visible="pad" length="short" direction="pas" function="dot"/>
<pin name="2" x="-2.54" y="0" visible="pad" length="short" direction="pas" function="dot"/>
</symbol>
<symbol name="PINHD4">
<wire x1="-6.35" y1="-5.08" x2="1.27" y2="-5.08" width="0.4064" layer="94"/>
<wire x1="1.27" y1="-5.08" x2="1.27" y2="7.62" width="0.4064" layer="94"/>
<wire x1="1.27" y1="7.62" x2="-6.35" y2="7.62" width="0.4064" layer="94"/>
<wire x1="-6.35" y1="7.62" x2="-6.35" y2="-5.08" width="0.4064" layer="94"/>
<text x="-6.35" y="8.255" size="1.778" layer="95">&gt;NAME</text>
<text x="-6.35" y="-7.62" size="1.778" layer="96">&gt;VALUE</text>
<pin name="1" x="-2.54" y="5.08" visible="pad" length="short" direction="pas" function="dot"/>
<pin name="2" x="-2.54" y="2.54" visible="pad" length="short" direction="pas" function="dot"/>
<pin name="3" x="-2.54" y="0" visible="pad" length="short" direction="pas" function="dot"/>
<pin name="4" x="-2.54" y="-2.54" visible="pad" length="short" direction="pas" function="dot"/>
</symbol>
</symbols>
<devicesets>
<deviceset name="PINHD-1X2" prefix="JP" uservalue="yes">
<description>&lt;b&gt;PIN HEADER&lt;/b&gt;</description>
<gates>
<gate name="G$1" symbol="PINHD2" x="0" y="0"/>
</gates>
<devices>
<device name="" package="1X02">
<connects>
<connect gate="G$1" pin="1" pad="1"/>
<connect gate="G$1" pin="2" pad="2"/>
</connects>
<technologies>
<technology name=""/>
</technologies>
</device>
<device name="/90" package="1X02/90">
<connects>
<connect gate="G$1" pin="1" pad="1"/>
<connect gate="G$1" pin="2" pad="2"/>
</connects>
<technologies>
<technology name=""/>
</technologies>
</device>
</devices>
</deviceset>
<deviceset name="PINHD-1X4" prefix="JP" uservalue="yes">
<description>&lt;b&gt;PIN HEADER&lt;/b&gt;</description>
<gates>
<gate name="A" symbol="PINHD4" x="0" y="0"/>
</gates>
<devices>
<device name="" package="1X04">
<connects>
<connect gate="A" pin="1" pad="1"/>
<connect gate="A" pin="2" pad="2"/>
<connect gate="A" pin="3" pad="3"/>
<connect gate="A" pin="4" pad="4"/>
</connects>
<technologies>
<technology name=""/>
</technologies>
</device>
<device name="/90" package="1X04/90">
<connects>
<connect gate="A" pin="1" pad="1"/>
<connect gate="A" pin="2" pad="2"/>
<connect gate="A" pin="3" pad="3"/>
<connect gate="A" pin="4" pad="4"/>
</connects>
<technologies>
<technology name=""/>
</technologies>
</device>
</devices>
</deviceset>
</devicesets>
</library>
</libraries>
<attributes>
</attributes>
<variantdefs>
</variantdefs>
<classes>
<class number="0" name="default" width="0" drill="0">
</class>
</classes>
<parts>
<part name="U$4" library="ESP32-DEVKITC" deviceset="ESP32DEVKITC" device=""/>
<part name="JP1" library="pinhead" deviceset="PINHD-1X2" device=""/>
<part name="JP2" library="pinhead" deviceset="PINHD-1X2" device=""/>
<part name="JP3" library="pinhead" deviceset="PINHD-1X2" device=""/>
<part name="JP4" library="pinhead" deviceset="PINHD-1X4" device=""/>
</parts>
<sheets>
<sheet>
<plain>
<text x="137.16" y="60.96" size="1.778" layer="91" rot="MR0">Running Board Middle</text>
<text x="33.02" y="53.34" size="1.778" layer="91">IMU</text>
<text x="114.3" y="76.2" size="1.778" layer="91">Running Board Right</text>
<text x="116.84" y="45.72" size="1.778" layer="91">Running Board Left</text>
</plain>
<instances>
<instance part="U$4" gate="G$1" x="76.2" y="55.88" rot="R90"/>
<instance part="JP1" gate="G$1" x="132.08" y="66.04"/>
<instance part="JP2" gate="G$1" x="132.08" y="53.34"/>
<instance part="JP3" gate="G$1" x="132.08" y="38.1"/>
<instance part="JP4" gate="A" x="33.02" y="45.72" rot="R180"/>
</instances>
<busses>
</busses>
<nets>
<net name="N$1" class="0">
<segment>
<pinref part="JP1" gate="G$1" pin="1"/>
<pinref part="U$4" gate="G$1" pin="IO35"/>
<wire x1="129.54" y1="68.58" x2="116.84" y2="68.58" width="0.1524" layer="91"/>
<wire x1="116.84" y1="68.58" x2="93.98" y2="45.72" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$2" class="0">
<segment>
<pinref part="JP2" gate="G$1" pin="1"/>
<pinref part="U$4" gate="G$1" pin="IO34"/>
<wire x1="129.54" y1="55.88" x2="106.68" y2="55.88" width="0.1524" layer="91"/>
<wire x1="106.68" y1="55.88" x2="93.98" y2="43.18" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$3" class="0">
<segment>
<pinref part="JP3" gate="G$1" pin="1"/>
<pinref part="U$4" gate="G$1" pin="SVN"/>
<wire x1="129.54" y1="40.64" x2="93.98" y2="40.64" width="0.1524" layer="91"/>
</segment>
</net>
<net name="GND" class="0">
<segment>
<pinref part="JP1" gate="G$1" pin="2"/>
<wire x1="129.54" y1="66.04" x2="121.92" y2="66.04" width="0.1524" layer="91"/>
<wire x1="121.92" y1="66.04" x2="119.38" y2="63.5" width="0.1524" layer="91"/>
<wire x1="119.38" y1="63.5" x2="119.38" y2="60.96" width="0.1524" layer="91"/>
<wire x1="119.38" y1="60.96" x2="121.92" y2="58.42" width="0.1524" layer="91"/>
<wire x1="121.92" y1="58.42" x2="139.7" y2="58.42" width="0.1524" layer="91"/>
<wire x1="139.7" y1="58.42" x2="144.78" y2="53.34" width="0.1524" layer="91"/>
<wire x1="144.78" y1="53.34" x2="144.78" y2="27.94" width="0.1524" layer="91"/>
<wire x1="144.78" y1="27.94" x2="139.7" y2="22.86" width="0.1524" layer="91"/>
<wire x1="139.7" y1="22.86" x2="58.42" y2="22.86" width="0.1524" layer="91"/>
<wire x1="58.42" y1="22.86" x2="53.34" y2="27.94" width="0.1524" layer="91"/>
<wire x1="53.34" y1="27.94" x2="53.34" y2="30.48" width="0.1524" layer="91"/>
<wire x1="53.34" y1="30.48" x2="55.88" y2="33.02" width="0.1524" layer="91"/>
<pinref part="JP2" gate="G$1" pin="2"/>
<wire x1="129.54" y1="53.34" x2="121.92" y2="53.34" width="0.1524" layer="91"/>
<wire x1="121.92" y1="53.34" x2="119.38" y2="50.8" width="0.1524" layer="91"/>
<wire x1="119.38" y1="50.8" x2="119.38" y2="48.26" width="0.1524" layer="91"/>
<wire x1="119.38" y1="48.26" x2="121.92" y2="45.72" width="0.1524" layer="91"/>
<wire x1="121.92" y1="45.72" x2="139.7" y2="45.72" width="0.1524" layer="91"/>
<wire x1="139.7" y1="45.72" x2="142.24" y2="43.18" width="0.1524" layer="91"/>
<wire x1="142.24" y1="43.18" x2="142.24" y2="27.94" width="0.1524" layer="91"/>
<wire x1="142.24" y1="27.94" x2="139.7" y2="25.4" width="0.1524" layer="91"/>
<wire x1="139.7" y1="25.4" x2="58.42" y2="25.4" width="0.1524" layer="91"/>
<wire x1="58.42" y1="25.4" x2="55.88" y2="27.94" width="0.1524" layer="91"/>
<wire x1="55.88" y1="27.94" x2="55.88" y2="30.48" width="0.1524" layer="91"/>
<pinref part="JP3" gate="G$1" pin="2"/>
<wire x1="129.54" y1="38.1" x2="119.38" y2="38.1" width="0.1524" layer="91"/>
<wire x1="119.38" y1="38.1" x2="109.22" y2="27.94" width="0.1524" layer="91"/>
<wire x1="109.22" y1="27.94" x2="60.96" y2="27.94" width="0.1524" layer="91"/>
<wire x1="60.96" y1="27.94" x2="58.42" y2="30.48" width="0.1524" layer="91"/>
<pinref part="U$4" gate="G$1" pin="GND@38"/>
<wire x1="58.42" y1="30.48" x2="58.42" y2="33.02" width="0.1524" layer="91"/>
<wire x1="55.88" y1="30.48" x2="58.42" y2="33.02" width="0.1524" layer="91"/>
<junction x="58.42" y="33.02"/>
<wire x1="55.88" y1="33.02" x2="58.42" y2="33.02" width="0.1524" layer="91"/>
<pinref part="JP4" gate="A" pin="2"/>
<wire x1="35.56" y1="43.18" x2="48.26" y2="43.18" width="0.1524" layer="91"/>
<wire x1="48.26" y1="43.18" x2="58.42" y2="33.02" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$5" class="0">
<segment>
<pinref part="U$4" gate="G$1" pin="3V3"/>
<wire x1="93.98" y1="33.02" x2="96.52" y2="30.48" width="0.1524" layer="91"/>
<wire x1="96.52" y1="30.48" x2="96.52" y2="20.32" width="0.1524" layer="91"/>
<wire x1="96.52" y1="20.32" x2="93.98" y2="17.78" width="0.1524" layer="91"/>
<wire x1="93.98" y1="17.78" x2="50.8" y2="17.78" width="0.1524" layer="91"/>
<wire x1="50.8" y1="17.78" x2="43.18" y2="25.4" width="0.1524" layer="91"/>
<wire x1="43.18" y1="25.4" x2="43.18" y2="38.1" width="0.1524" layer="91"/>
<wire x1="43.18" y1="38.1" x2="40.64" y2="40.64" width="0.1524" layer="91"/>
<pinref part="JP4" gate="A" pin="1"/>
<wire x1="40.64" y1="40.64" x2="35.56" y2="40.64" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$7" class="0">
<segment>
<pinref part="U$4" gate="G$1" pin="IO21"/>
<pinref part="JP4" gate="A" pin="4"/>
<wire x1="58.42" y1="45.72" x2="55.88" y2="48.26" width="0.1524" layer="91"/>
<wire x1="55.88" y1="48.26" x2="35.56" y2="48.26" width="0.1524" layer="91"/>
</segment>
</net>
<net name="N$8" class="0">
<segment>
<pinref part="U$4" gate="G$1" pin="IO22"/>
<wire x1="58.42" y1="38.1" x2="50.8" y2="45.72" width="0.1524" layer="91"/>
<pinref part="JP4" gate="A" pin="3"/>
<wire x1="50.8" y1="45.72" x2="35.56" y2="45.72" width="0.1524" layer="91"/>
</segment>
</net>
</nets>
</sheet>
</sheets>
</schematic>
</drawing>
</eagle>
