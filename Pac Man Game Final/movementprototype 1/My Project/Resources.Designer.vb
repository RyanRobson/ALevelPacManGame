﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("PacManGame.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        Friend ReadOnly Property Maze1() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Maze1", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Pac_Man_Google_Game() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Pac-Man-Google-Game", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Pac_Man_Google_Game_Down() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Pac-Man-Google-Game Down", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Pac_Man_Google_Game_Left() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Pac-Man-Google-Game Left", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Pac_Man_Google_Game_Up() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Pac-Man-Google-Game Up", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property PacMan_Blinky() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PacMan Blinky", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property PacMan_Clyde() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PacMan Clyde", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property PacMan_Frightened() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PacMan Frightened", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property PacMan_Inky() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PacMan Inky", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property PAcMan_Pinky() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("PAcMan Pinky", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Vulnerableghostbig() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Vulnerableghostbig", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        Friend ReadOnly Property Vulnerableghostbig1() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Vulnerableghostbig1", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
    End Module
End Namespace
