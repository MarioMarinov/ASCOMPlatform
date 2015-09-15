﻿'-----------------------------------------------------------------------
' <summary>Defines the ISafetyMonitor Interface</summary>
'-----------------------------------------------------------------------
Imports System.Runtime.InteropServices
''' <summary>
''' Defines the IObservingConditions Interface.
''' This interface provides a limited set of values that are useful
''' for astronomical purposes for things such as determining if it is safe to open or operate the observing system
''' and for recording astronomical data or determining refraction corrections.
''' </summary>
''' <remarks>It is NOT intended as a general purpose environmental sensor system.
''' The <see cref="IObservingConditions.Action">Action</see> method and 
''' <see cref="IObservingConditions.SupportedActions">SupportedActions</see> property 
''' can be used to add driver specific sensors.
''' </remarks>
<ComVisible(True), Guid("06E9F8D9-E85C-4B2B-BC84-6F2EF6B3E779"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)>
Public Interface IObservingConditions

#Region "Common Methods"
    'IAscomDriver Methods

    ''' <summary>
    ''' Set True to connect to the device hardware. Set False to disconnect from the device hardware.
    ''' You can also read the property to check whether it is connected. This reports the current hardware state.
    ''' </summary>
    ''' <value><c>true</c> if connected to the hardware; otherwise, <c>false</c>.</value>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks>
    ''' <p style="color:red"><b>Must be implemented</b></p>Do not use a NotConnectedException here, that exception is for use in other methods that require a connection in order to succeed.
    ''' <para>The Connected property sets and reports the state of connection to the device hardware.
    ''' For a hub this means that Connected will be true when the first driver connects and will only be set to false
    ''' when all drivers have disconnected.  A second driver may find that Connected is already true and
    ''' setting Connected to false does not report Connected as false.  This is not an error because the physical state is that the
    ''' hardware connection is still true.</para>
    ''' <para>Multiple calls setting Connected to true or false will not cause an error.</para>
    ''' </remarks>
    Property Connected() As Boolean

    ''' <summary>
    ''' Returns a description of the device, such as manufacturer and modelnumber. Any ASCII characters may be used. 
    ''' </summary>
    ''' <value>The description.</value>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks><p style="color:red"><b>Must be implemented</b></p> </remarks>
    ReadOnly Property Description() As String

    ''' <summary>
    ''' Descriptive and version information about this ASCOM driver.
    ''' </summary>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks>
    ''' <p style="color:red"><b>Must be implemented</b></p> This string may contain line endings and may be hundreds to thousands of characters long.
    ''' It is intended to display detailed information on the ASCOM driver, including version and copyright data.
    ''' See the <see cref="Description" /> property for information on the device itself.
    ''' To get the driver version in a parseable string, use the <see cref="DriverVersion" /> property.
    ''' </remarks>
    ReadOnly Property DriverInfo() As String

    ''' <summary>
    ''' A string containing only the major and minor version of the driver.
    ''' </summary>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks><p style="color:red"><b>Must be implemented</b></p> This must be in the form "n.n".
    ''' It should not to be confused with the <see cref="InterfaceVersion" /> property, which is the version of this specification supported by the 
    ''' driver.
    ''' </remarks>
    ReadOnly Property DriverVersion() As String

    ''' <summary>
    ''' The interface version number that this device supports. Should return 2 for this interface version.
    ''' </summary>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks><p style="color:red"><b>Must be implemented</b></p> Clients can detect legacy V1 drivers by trying to read ths property.
    ''' If the driver raises an error, it is a V1 driver. V1 did not specify this property. A driver may also return a value of 1. 
    ''' In other words, a raised error or a return value of 1 indicates that the driver is a V1 driver.
    ''' </remarks>
    ReadOnly Property InterfaceVersion() As Short

    ''' <summary>
    ''' The short name of the driver, for display purposes
    ''' </summary>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks><p style="color:red"><b>Must be implemented</b></p> </remarks>
    ReadOnly Property Name() As String

    ''' <summary>
    ''' Launches a configuration dialog box for the driver.  The call will not return
    ''' until the user clicks OK or cancel manually.
    ''' </summary>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks><p style="color:red"><b>Must be implemented</b></p> </remarks>
    Sub SetupDialog()

    'DeviceControl Methods

    ''' <summary>
    ''' Invokes the specified device-specific action.
    ''' </summary>
    ''' <param name="ActionName">
    ''' A well known name agreed by interested parties that represents the action to be carried out. 
    ''' </param>
    ''' <param name="ActionParameters">List of required parameters or an <see cref="String.Empty">Empty String</see> if none are required.
    ''' </param>
    ''' <returns>A string response. The meaning of returned strings is set by the driver author.</returns>
    ''' <exception cref="ASCOM.MethodNotImplementedException">Throws this exception if no actions are suported.</exception>
    ''' <exception cref="ASCOM.ActionNotImplementedException">It is intended that the SupportedActions method will inform clients 
    ''' of driver capabilities, but the driver must still throw an ASCOM.ActionNotImplemented exception if it is asked to 
    ''' perform an action that it does not support.</exception>
    ''' <exception cref="NotConnectedException">If the driver is not connected.</exception>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <example>Suppose filter wheels start to appear with automatic wheel changers; new actions could 
    ''' be “FilterWheel:QueryWheels” and “FilterWheel:SelectWheel”. The former returning a 
    ''' formatted list of wheel names and the second taking a wheel name and making the change, returning appropriate 
    ''' values to indicate success or failure.
    ''' </example>
    ''' <remarks><p style="color:red"><b>Can throw a not implemented exception</b></p> 
    ''' This method is intended for use in all current and future device types and to avoid name clashes, management of action names 
    ''' is important from day 1. A two-part naming convention will be adopted - <b>DeviceType:UniqueActionName</b> where:
    ''' <list type="bullet">
    ''' <item><description>DeviceType is the same value as would be used by <see cref="ASCOM.Utilities.Chooser.DeviceType"/> e.g. Telescope, Camera, Switch etc.</description></item>
    ''' <item><description>UniqueActionName is a single word, or multiple words joined by underscore characters, that sensibly describes the action to be performed.</description></item>
    ''' </list>
    ''' <para>
    ''' It is recommended that UniqueActionNames should be a maximum of 16 characters for legibility.
    ''' Should the same function and UniqueActionName be supported by more than one type of device, the reserved DeviceType of 
    ''' “General” will be used. Action names will be case insensitive, so FilterWheel:SelectWheel, filterwheel:selectwheel 
    ''' and FILTERWHEEL:SELECTWHEEL will all refer to the same action.</para>
    ''' <para>The names of all supported actions must be returned in the <see cref="SupportedActions"/> property.</para>
    ''' <para>For ObservingConditions drivers the following conventions are recommended:
    ''' <list type="bullet">
    ''' <item>The "ActionName" should be the name of a sensor in a form that makes sense to the user.
    ''' This must not be changed in the driver.</item>
    ''' <item>The "ActionParameter" should be "Value" to return the sensor value and 
    ''' "Description" to return the sensor description. 
    ''' The description must return a valid description, even if not connected.</item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Function Action(ByVal ActionName As String, ByVal ActionParameters As String) As String

    ''' <summary>
    ''' Returns the list of action names supported by this driver.
    ''' </summary>
    ''' <value>An ArrayList of strings (SafeArray collection) containing the names of supported actions.</value>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks><p style="color:red"><b>Must be implemented</b></p> This method must return an empty arraylist if no actions are supported. Please do not throw a 
    ''' <see cref="ASCOM.PropertyNotImplementedException" />.
    ''' <para>This is an aid to client authors and testers who would otherwise have to repeatedly poll the driver to determine its capabilities. 
    ''' Returned action names may be in mixed case to enhance presentation but  will be recognised case insensitively in 
    ''' the <see cref="Action">Action</see> method.</para>
    '''<para>An array list collection has been selected as the vehicle for  action names in order to make it easier for clients to
    ''' determine whether a particular action is supported. This is easily done through the Contains method. Since the
    ''' collection is also ennumerable it is easy to use constructs such as For Each ... to operate on members without having to be concerned 
    ''' about hom many members are in the collection. </para>
    ''' <para>Collections have been used in the Telescope specification for a number of years and are known to be compatible with COM. Within .NET
    ''' the ArrayList is the correct implementation to use as the .NET Generic methods are not compatible with COM.</para>
    ''' <para>See <see cref="Action">Action</see> for advice on how th implement this for ObservingConditions drivers.</para>
    ''' </remarks>
    ReadOnly Property SupportedActions() As ArrayList

    ''' <summary>
    ''' Transmits an arbitrary string to the device and does not wait for a response.
    ''' Optionally, protocol framing characters may be added to the string before transmission.
    ''' </summary>
    ''' <param name="Command">The literal command string to be transmitted.</param>
    ''' <param name="Raw">
    ''' if set to <c>true</c> the string is transmitted 'as-is'.
    ''' If set to <c>false</c> then protocol framing characters may be added prior to transmission.
    ''' </param>
    ''' <exception cref="MethodNotImplementedException">If the method is not implemented</exception>
    ''' <exception cref="NotConnectedException">If the driver is not connected.</exception>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks><p style="color:red"><b>Can throw a not implemented exception</b></p> </remarks>
    Sub CommandBlind(ByVal Command As String, Optional ByVal Raw As Boolean = False)

    ''' <summary>
    ''' Transmits an arbitrary string to the device and waits for a boolean response.
    ''' Optionally, protocol framing characters may be added to the string before transmission.
    ''' </summary>
    ''' <param name="Command">The literal command string to be transmitted.</param>
    ''' <param name="Raw">
    ''' if set to <c>true</c> the string is transmitted 'as-is'.
    ''' If set to <c>false</c> then protocol framing characters may be added prior to transmission.
    ''' </param>
    ''' <returns>
    ''' Returns the interpreted boolean response received from the device.
    ''' </returns>
    ''' <exception cref="MethodNotImplementedException">If the method is not implemented</exception>
    ''' <exception cref="NotConnectedException">If the driver is not connected.</exception>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks><p style="color:red"><b>Can throw a not implemented exception</b></p> </remarks>
    Function CommandBool(ByVal Command As String, Optional ByVal Raw As Boolean = False) As Boolean

    ''' <summary>
    ''' Transmits an arbitrary string to the device and waits for a string response.
    ''' Optionally, protocol framing characters may be added to the string before transmission.
    ''' </summary>
    ''' <param name="Command">The literal command string to be transmitted.</param>
    ''' <param name="Raw">
    ''' if set to <c>true</c> the string is transmitted 'as-is'.
    ''' If set to <c>false</c> then protocol framing characters may be added prior to transmission.
    ''' </param>
    ''' <returns>
    ''' Returns the string response received from the device.
    ''' </returns>
    ''' <exception cref="MethodNotImplementedException">If the method is not implemented</exception>
    ''' <exception cref="NotConnectedException">If the driver is not connected.</exception>
    ''' <exception cref="DriverException">Must throw an exception if the call was not successful</exception>
    ''' <remarks><p style="color:red"><b>Can throw a not implemented exception</b></p> </remarks>
    Function CommandString(ByVal Command As String, Optional ByVal Raw As Boolean = False) As String

    ''' <summary>
    ''' Dispose the late-bound interface, if needed. Will release it via COM
    ''' if it is a COM object, else if native .NET will just dereference it
    ''' for GC.
    ''' </summary>
    Sub Dispose()

#End Region

#Region "Device Properties"
    ''' <summary>
    ''' Gets And sets the time period over which observations will be averaged
    ''' </summary>
    ''' <value>Time period (hours) over which to average sensor readings</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks>
    ''' Time period (hours) over which the property values will be averaged 0.0 = current value, 0.5= average for the last 30 minutes, 1.0 = average for the last hour
    ''' </remarks>
    Property AveragePeriod As Double

    ''' <summary>
    ''' Amount of sky obscured by cloud
    ''' </summary>
    ''' <value>percentage of the sky covered by cloud</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks>0%= clear sky, 100% = 100% cloud coverage</remarks>
    ReadOnly Property CloudCover As Double

    ''' <summary>
    ''' Atmospheric dew point at the observatory
    ''' </summary>
    ''' <value>Atmospheric dew point reported in °C.</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks>Normally optional but mandatory if <see cref="Humidity"/> Is provided</remarks>
    ReadOnly Property DewPoint As Double

    ''' <summary>
    ''' Atmospheric humidity at the observatory
    ''' </summary>
    ''' <value>Atmospheric humidity (%)</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks>Normally optional but mandatory if <see cref="DewPoint"/> Is provided</remarks>   
    ReadOnly Property Humidity As Double

    ''' <summary>
    ''' Atmospheric pressure at the observatory
    ''' </summary>
    ''' <value>Atmospheric presure at the observatory (hPa)</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks>This must be the pressure at the observatory and not the "reduced" pressure at sea level.
    ''' Please check whether your pressure sensor delivers local pressure or sea level pressure
    ''' and adjust if required to observatory pressure.</remarks>
    ReadOnly Property Pressure As Double

    ''' <summary>
    ''' Rain rate at the observatory
    ''' </summary>
    ''' <value>Rain rate (mm / hour)</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks>This property can be interpreted as 0.0 = Dry any positive nonzero value = wet.</remarks>
    ReadOnly Property RainRate As Double

    ''' <summary>
    ''' Sky brightness at the observatory
    ''' </summary>
    ''' <value>Sky brightness (Lux)</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks></remarks>
    ReadOnly Property SkyBrightness As Double

    ''' <summary>
    ''' Sky quality at the observatory
    ''' </summary>
    ''' <value>Sky quality measured in magnitudes per square arc second</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks></remarks>
    ReadOnly Property SkyQuality As Double

    ''' <summary>
    ''' Seeing at the observatory as FWHM in arc secs.
    ''' </summary>
    ''' <value>Seeing reported as star full width half magnitude (arc seconds)</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ReadOnly Property SkySeeing As Double

    ''' <summary>
    ''' Sky temperature at the observatory
    ''' </summary>
    ''' <value>Sky temperature in °C</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks>This is expected to be returned by an infra ref sensor looking at the sky.
    ''' The lower the temperature the more the sky is likely to be clear.</remarks>
    ReadOnly Property SkyTemperature As Double

    ''' <summary>
    ''' Temperature at the observatory
    ''' </summary>
    ''' <value>Temperature in °C</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks>This is expected to be the ambient temperature.</remarks>
    ReadOnly Property Temperature As Double

    ''' <summary>
    ''' Wind direction at the observatory
    ''' </summary>
    ''' <value>Wind direction (degrees, 0..360.0)</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks>0..360.0, 360=N, 180=S, 90=E, 270=W. When there Is no wind the driver will return a value of 0 for wind direction</remarks>
    ReadOnly Property WindDirection As Double

    ''' <summary>
    ''' Peak 3 second wind gust at the observatory over the last 2 minutes
    ''' </summary>
    ''' <value>Wind gust (m/s) Peak 3 second wind speed over the last 2 minutes</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks></remarks>
    ReadOnly Property WindGust As Double

    ''' <summary>
    ''' Wind speed at the observatory
    ''' </summary>
    ''' <value>Wind speed (m/s)</value>
    ''' <exception cref="PropertyNotImplementedException">If this property is not available.</exception>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks></remarks>
    ReadOnly Property WindSpeed As Double
#End Region

#Region "Device Methods"

    ''' <summary>
    ''' Provides the time since the sensor value was last updated
    ''' </summary>
    ''' <param name="PropertyName">Name of the property whose time since last update is required</param>
    ''' <returns>Time in seconds since the last sensor update for this property</returns>
    ''' <exception cref="NotConnectedException">If the device is not connected and this information is only available when connected.</exception>
    ''' <remarks>PropertyName should be one of the sensor properties Or an empty string to get the last update
    ''' of any parameter. A negative value indicates no valid value ever received.</remarks>
    Function TimeSinceLastUpdate(PropertyName As String) As Double

    ''' <summary>
    ''' Provides a description of the sensor providing the requested property
    ''' </summary>
    ''' <param name="PropertyName">Name of the sensor whose description is required</param>
    ''' <returns>The description of the specified sensor.</returns>
    ''' <exception cref="MethodNotImplementedException">If the sensor is not available.</exception>
    ''' <remarks>PropertyName must be the name of one of the sensor properties implemented by this driver.
    ''' This must return a valid string even if the driver is not connected so that
    ''' applications can use this to determine what sensors are available.</remarks>
    Function SensorDescription(PropertyName As String) As String

    ''' <summary>
    ''' Forces the driver to immediatley query its atatched hardware to refersh sensor values
    ''' </summary>
    ''' <exception cref="NotConnectedException">If the device is not connected.</exception>
    ''' <exception cref="MethodNotImplementedException">If this method is not available.</exception>
    Sub Refresh()
#End Region

End Interface
