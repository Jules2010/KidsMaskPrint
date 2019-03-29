Module JunglePack
    Function JunglePack() As FacePartStuctureDataFile

        Dim strPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName())

        Dim RhinoHorns As New Part   'ok2
        With RhinoHorns
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Rhino Horns"
            .FullImage = Image.FromFile(strPath & "\PackJungle\rhino horns.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\rhino horns.bmp") '##
            .LeftPart = New Point(193, 3)
            .BothParts = False
        End With
        Dim RhinoEye As New Part   'ok2
        With RhinoEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Rhino"
            .FullImage = Image.FromFile(strPath & "\PackJungle\rhino eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\rhino eye.bmp") '##
            .LeftPart = New Point(130, 270)
            .RightPart = New Point(354, 270)
            .BothParts = True
        End With
        Dim RhinoNose As New Part   'ok2
        With RhinoNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Rhino"
            .FullImage = Image.FromFile(strPath & "\PackJungle\rhino nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\rhino nose.bmp") '##
            .LeftPart = New Point(177, 414)
            .RightPart = New Point(293, 414)
            .BothParts = True
        End With
        Dim RhinoEar As New Part   'ok2
        With RhinoEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Rhino"
            .FullImage = Image.FromFile(strPath & "\PackJungle\rhino ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\rhino ear.bmp") '##
            .LeftPart = New Point(37, 26)
            .RightPart = New Point(376, 26)
            .BothParts = True
        End With
        Dim ChimpEye As New Part   'ok2
        With ChimpEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Chimp"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chimp eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chimp eye.bmp") '##
            .LeftPart = New Point(173, 185)
            .RightPart = New Point(273, 185)
            .BothParts = True
        End With
        Dim ParrotEye As New Part   'ok2
        With ParrotEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Parrot"
            .FullImage = Image.FromFile(strPath & "\PackJungle\parrot eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\parrot eye.bmp") '##
            .LeftPart = New Point(116, 88)
            .RightPart = New Point(297, 88)
            .BothParts = True
        End With
        Dim ParrotMouth As New Part   'ok2
        With ParrotMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Parrot"
            .FullImage = Image.FromFile(strPath & "\PackJungle\parrot mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\parrot mouth.bmp") '##
            .LeftPart = New Point(132, 319)
            .BothParts = False
        End With
        Dim ParrotBeak As New Part   'ok2
        With ParrotBeak
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Parrot"
            .FullImage = Image.FromFile(strPath & "\PackJungle\parrot beak.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\parrot beak.bmp") '##
            .LeftPart = New Point(86, 139)
            .BothParts = False
        End With
        Dim ParrotOutline As New Part   'ok2
        With ParrotOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Parrot"
            .FullImage = Image.FromFile(strPath & "\PackJungle\parrot outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\parrot outline.bmp") '##
            .LeftPart = New Point(25, 13)
            .BothParts = False
        End With
        Dim PantherWhiskers As New Part   'ok2
        With PantherWhiskers
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Panther Whiskers"
            .FullImage = Image.FromFile(strPath & "\PackJungle\panther whiskers.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\panther whiskers.bmp") '##
            .LeftPart = New Point(45, 336)
            .RightPart = New Point(321, 336)
            .BothParts = True
        End With
        Dim PantherBridge As New Part   'ok2
        With PantherBridge
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Panther Bridge"
            .FullImage = Image.FromFile(strPath & "\PackJungle\panther bridge.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\panther bridge.bmp") '##
            .LeftPart = New Point(229, 122)
            .BothParts = False
        End With
        Dim PantherEyebrow As New Part   'ok2
        With PantherEyebrow
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Panther Eyebrow"
            .FullImage = Image.FromFile(strPath & "\PackJungle\panther eyebrow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\panther eyebrow.bmp") '##
            .LeftPart = New Point(124, 219)
            .RightPart = New Point(297, 219)
            .BothParts = True
        End With
        Dim ChimpEar As New Part   'ok2
        With ChimpEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Chimp"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chimp ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chimp ear.bmp") '##
            .LeftPart = New Point(0, 93)
            .RightPart = New Point(403, 93)
            .BothParts = True
        End With
        Dim ChimpNose As New Part   'ok2
        With ChimpNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Chimp"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chimp nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chimp nose.bmp") '##
            .LeftPart = New Point(205, 277)
            .BothParts = False
        End With
        Dim HippoEar As New Part   'ok2
        With HippoEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Hippo"
            .FullImage = Image.FromFile(strPath & "\PackJungle\hippo ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\hippo ear.bmp") '##
            .LeftPart = New Point(54, 7)
            .RightPart = New Point(379, 7)
            .BothParts = True
        End With
        Dim ChimpBrow As New Part   'ok2
        With ChimpBrow
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Chimp Brow"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chimp brow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chimp brow.bmp") '##
            .LeftPart = New Point(124, 126)
            .BothParts = False
        End With
        Dim ZebraEar As New Part   'ok2
        With ZebraEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Zebra"
            .FullImage = Image.FromFile(strPath & "\PackJungle\zebra ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\zebra ear.bmp") '##
            .LeftPart = New Point(69, 0)
            .RightPart = New Point(294, 0)
            .BothParts = True
        End With
        Dim ChimpOutline As New Part   'ok2
        With ChimpOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Chimp"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chimp outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chimp outline.bmp") '##
            .LeftPart = New Point(0, 10)
            .BothParts = False
        End With
        Dim AnteaterOutline As New Part   'ok2
        With AnteaterOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Anteater"
            .FullImage = Image.FromFile(strPath & "\PackJungle\anteater outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\anteater outline.bmp") '##
            .LeftPart = New Point(7, 42)
            .BothParts = False
        End With
        Dim ChimpMouth As New Part   'ok2
        With ChimpMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Chimp"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chimp mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chimp mouth.bmp") '##
            .LeftPart = New Point(154, 236)
            .BothParts = False
        End With
        Dim ZebraEye As New Part   'ok2
        With ZebraEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Zebra"
            .FullImage = Image.FromFile(strPath & "\PackJungle\zebra eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\zebra eye.bmp") '##
            .LeftPart = New Point(132, 225)
            .RightPart = New Point(337, 225)
            .BothParts = True
        End With
        Dim ChimpChin As New Part   'ok2
        With ChimpChin
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Chimp Chin"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chimp chin.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chimp chin.bmp") '##
            .LeftPart = New Point(151, 363)
            .BothParts = False
        End With
        Dim HippoEye As New Part   'ok2
        With HippoEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Hippo"
            .FullImage = Image.FromFile(strPath & "\PackJungle\hippo eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\hippo eye.bmp") '##
            .LeftPart = New Point(77, 55)
            .RightPart = New Point(309, 55)
            .BothParts = True
        End With
        Dim PantherEar As New Part   'ok2
        With PantherEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Panther"
            .FullImage = Image.FromFile(strPath & "\PackJungle\panther ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\panther ear.bmp") '##
            .LeftPart = New Point(46, 7)
            .RightPart = New Point(308, 7)
            .BothParts = True
        End With
        Dim PantherEye As New Part   'ok2
        With PantherEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Panther"
            .FullImage = Image.FromFile(strPath & "\PackJungle\panther eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\panther eye.bmp") '##
            .LeftPart = New Point(128, 258)
            .RightPart = New Point(293, 258)
            .BothParts = True
        End With
        Dim GiraffeOutline As New Part   'ok2
        With GiraffeOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Giraffe"
            .FullImage = Image.FromFile(strPath & "\PackJungle\giraffe outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\giraffe outline.bmp") '##
            .LeftPart = New Point(8, 19)
            .BothParts = False
        End With
        Dim GiraffeNose As New Part   'ok2
        With GiraffeNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Giraffe"
            .FullImage = Image.FromFile(strPath & "\PackJungle\giraffe nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\giraffe nose.bmp") '##
            .LeftPart = New Point(192, 309)
            .BothParts = False
        End With
        Dim LionBrow As New Part   'ok2
        With LionBrow
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Lion Brow"
            .FullImage = Image.FromFile(strPath & "\PackJungle\lion brow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\lion brow.bmp") '##
            .LeftPart = New Point(233, 107)
            .BothParts = False
        End With
        Dim PantherNose As New Part   'ok2
        With PantherNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Panther"
            .FullImage = Image.FromFile(strPath & "\PackJungle\panther nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\panther nose.bmp") '##
            .LeftPart = New Point(162, 374)
            .BothParts = False
        End With
        Dim PantherOutline As New Part   'ok2
        With PantherOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Panther"
            .FullImage = Image.FromFile(strPath & "\PackJungle\panther outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\panther outline.bmp") '##
            .LeftPart = New Point(45, 7)
            .BothParts = False
        End With
        Dim GiraffeEye As New Part   'ok2
        With GiraffeEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Giraffe"
            .FullImage = Image.FromFile(strPath & "\PackJungle\giraffe eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\giraffe eye.bmp") '##
            .LeftPart = New Point(95, 229)
            .RightPart = New Point(338, 229)
            .BothParts = True
        End With
        Dim GiraffeEar As New Part   'ok2
        With GiraffeEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Giraffe"
            .FullImage = Image.FromFile(strPath & "\PackJungle\giraffe ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\giraffe ear.bmp") '##
            .LeftPart = New Point(8, 86)
            .RightPart = New Point(351, 86)
            .BothParts = True
        End With
        Dim LionEar As New Part   'ok2
        With LionEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Lion"
            .FullImage = Image.FromFile(strPath & "\PackJungle\lion ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\lion ear.bmp") '##
            .LeftPart = New Point(43, 92)
            .RightPart = New Point(427, 92)
            .BothParts = True
        End With
        Dim GorillaOutline As New Part   'ok2
        With GorillaOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Gorilla"
            .FullImage = Image.FromFile(strPath & "\PackJungle\gorilla outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\gorilla outline.bmp") '##
            .LeftPart = New Point(50, 5)
            .BothParts = False
        End With
        Dim LionEye As New Part   'ok2
        With LionEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Lion"
            .FullImage = Image.FromFile(strPath & "\PackJungle\lion eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\lion eye.bmp") '##
            .LeftPart = New Point(158, 170)
            .RightPart = New Point(296, 170)
            .BothParts = True
        End With
        Dim GorillaLines As New Part   'ok2
        With GorillaLines
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Gorilla Lines"
            .FullImage = Image.FromFile(strPath & "\PackJungle\gorilla lines.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\gorilla lines.bmp") '##
            .LeftPart = New Point(143, 39)
            .BothParts = False
        End With
        Dim GorillaBrow As New Part   'ok2
        With GorillaBrow
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Gorilla Brow"
            .FullImage = Image.FromFile(strPath & "\PackJungle\gorilla brow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\gorilla brow.bmp") '##
            .LeftPart = New Point(130, 146)
            .BothParts = False
        End With
        Dim GorillaMouth As New Part   'ok2
        With GorillaMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Gorilla"
            .FullImage = Image.FromFile(strPath & "\PackJungle\gorilla mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\gorilla mouth.bmp") '##
            .LeftPart = New Point(165, 341)
            .BothParts = False
        End With
        Dim GorillaNose As New Part   'ok2
        With GorillaNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Gorilla"
            .FullImage = Image.FromFile(strPath & "\PackJungle\gorilla nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\gorilla nose.bmp") '##
            .LeftPart = New Point(208, 267)
            .BothParts = False
        End With
        Dim GorillaEye As New Part   'ok2
        With GorillaEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Gorilla"
            .FullImage = Image.FromFile(strPath & "\PackJungle\gorilla eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\gorilla eye.bmp") '##
            .LeftPart = New Point(176, 200)
            .RightPart = New Point(293, 200)
            .BothParts = True
        End With
        Dim ZebraMane As New Part   'ok2
        With ZebraMane
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Zebra Mane"
            .FullImage = Image.FromFile(strPath & "\PackJungle\zebra mane.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\zebra mane.bmp") '##
            .LeftPart = New Point(205, 6)
            .BothParts = False
        End With
        Dim ZebraNose As New Part   'ok2
        With ZebraNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Zebra"
            .FullImage = Image.FromFile(strPath & "\PackJungle\zebra nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\zebra nose.bmp") '##
            .LeftPart = New Point(189, 415)
            .RightPart = New Point(276, 415)
            .BothParts = True
        End With
        Dim ZebraOutline As New Part   'ok2
        With ZebraOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Zebra"
            .FullImage = Image.FromFile(strPath & "\PackJungle\zebra outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\zebra outline.bmp") '##
            .LeftPart = New Point(69, 0)
            .BothParts = False
        End With
        Dim CrocodileEye As New Part   'ok2
        With CrocodileEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Crocodile"
            .FullImage = Image.FromFile(strPath & "\PackJungle\crocodile eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\crocodile eye.bmp") '##
            .LeftPart = New Point(86, 29)
            .RightPart = New Point(366, 29)
            .BothParts = True
        End With
        Dim CrocodileOutline As New Part   'ok2
        With CrocodileOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Crocodile"
            .FullImage = Image.FromFile(strPath & "\PackJungle\crocodile outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\crocodile outline.bmp") '##
            .LeftPart = New Point(22, 9)
            .BothParts = False
        End With
        Dim CrocodileTeeth As New Part   'ok2
        With CrocodileTeeth
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Crocodile Teeth"
            .FullImage = Image.FromFile(strPath & "\PackJungle\crocodile teeth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\crocodile teeth.bmp") '##
            .LeftPart = New Point(60, 158)
            .BothParts = False
        End With
        Dim CrocodileBrow As New Part   'ok2
        With CrocodileBrow
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Crocodile Brow"
            .FullImage = Image.FromFile(strPath & "\PackJungle\crocodile brow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\crocodile brow.bmp") '##
            .LeftPart = New Point(80, 30)
            .BothParts = False
        End With
        Dim ElephantEar As New Part   'ok2
        With ElephantEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Elephant"
            .FullImage = Image.FromFile(strPath & "\PackJungle\elephant ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\elephant ear.bmp") '##
            .LeftPart = New Point(2, 25)
            .RightPart = New Point(310, 25)
            .BothParts = True
        End With
        Dim ElephantTusk As New Part   'ok2
        With ElephantTusk
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Elephant Tusk"
            .FullImage = Image.FromFile(strPath & "\PackJungle\elephant tusk.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\elephant tusk.bmp") '##
            .LeftPart = New Point(85, 328)
            .RightPart = New Point(324, 328)
            .BothParts = True
        End With
        Dim ElephantOutline As New Part   'ok2
        With ElephantOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Elephant"
            .FullImage = Image.FromFile(strPath & "\PackJungle\elephant outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\elephant outline.bmp") '##
            .LeftPart = New Point(2, 25)
            .BothParts = False
        End With
        Dim ElephantEye As New Part   'ok2
        With ElephantEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Elephant"
            .FullImage = Image.FromFile(strPath & "\PackJungle\elephant eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\elephant eye.bmp") '##
            .LeftPart = New Point(156, 179)
            .RightPart = New Point(326, 179)
            .BothParts = True
        End With
        Dim HippoOutline As New Part   'ok2
        With HippoOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Hippo"
            .FullImage = Image.FromFile(strPath & "\PackJungle\hippo outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\hippo outline.bmp") '##
            .LeftPart = New Point(38, 5)
            .BothParts = False
        End With
        Dim HippoNose As New Part   'ok2
        With HippoNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Hippo"
            .FullImage = Image.FromFile(strPath & "\PackJungle\hippo nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\hippo nose.bmp") '##
            .LeftPart = New Point(181, 287)
            .BothParts = False
        End With
        Dim LionMouth As New Part   'ok2
        With LionMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Lion"
            .FullImage = Image.FromFile(strPath & "\PackJungle\lion mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\lion mouth.bmp") '##
            .LeftPart = New Point(143, 238)
            .BothParts = False
        End With
        Dim LionOutline As New Part   'ok2
        With LionOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Lion"
            .FullImage = Image.FromFile(strPath & "\PackJungle\lion outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\lion outline.bmp") '##
            .LeftPart = New Point(8, 17)
            .BothParts = False
        End With
        Dim PorcupineOutline As New Part   'ok2
        With PorcupineOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Porcupine"
            .FullImage = Image.FromFile(strPath & "\PackJungle\porcupine outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\porcupine outline.bmp") '##
            .LeftPart = New Point(3, 48)
            .BothParts = False
        End With
        Dim PorcupineBrow As New Part   'ok2
        With PorcupineBrow
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Porcupine Brow"
            .FullImage = Image.FromFile(strPath & "\PackJungle\porcupine brow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\porcupine brow.bmp") '##
            .LeftPart = New Point(149, 136)
            .BothParts = False
        End With
        Dim PorcupineEye As New Part   'ok2
        With PorcupineEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Porcupine"
            .FullImage = Image.FromFile(strPath & "\PackJungle\porcupine eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\porcupine eye.bmp") '##
            .LeftPart = New Point(167, 238)
            .RightPart = New Point(313, 238)
            .BothParts = True
        End With
        Dim PorcupineMouth As New Part   'ok2
        With PorcupineMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Porcupine"
            .FullImage = Image.FromFile(strPath & "\PackJungle\porcupine mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\porcupine mouth.bmp") '##
            .LeftPart = New Point(180, 289)
            .BothParts = False
        End With
        Dim PorcupineCheek As New Part   'ok2
        With PorcupineCheek
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Porcupine Cheek"
            .FullImage = Image.FromFile(strPath & "\PackJungle\porcupine cheek.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\porcupine cheek.bmp") '##
            .LeftPart = New Point(80, 252)
            .RightPart = New Point(340, 252)
            .BothParts = True
        End With
        Dim AnteaterEye As New Part   'ok2
        With AnteaterEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Anteater"
            .FullImage = Image.FromFile(strPath & "\PackJungle\anteater eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\anteater eye.bmp") '##
            .LeftPart = New Point(142, 187)
            .RightPart = New Point(326, 187)
            .BothParts = True
        End With
        Dim AnteaterNose As New Part   'ok2
        With AnteaterNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Anteater"
            .FullImage = Image.FromFile(strPath & "\PackJungle\anteater nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\anteater nose.bmp") '##
            .LeftPart = New Point(192, 303)
            .BothParts = False
        End With
        Dim AnteaterEar As New Part   'ok2
        With AnteaterEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Anteater"
            .FullImage = Image.FromFile(strPath & "\PackJungle\anteater ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\anteater ear.bmp") '##
            .LeftPart = New Point(7, 62)
            .RightPart = New Point(357, 62)
            .BothParts = True
        End With
        Dim ArmadilloOutline As New Part   'ok2
        With ArmadilloOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Armadillo"
            .FullImage = Image.FromFile(strPath & "\PackJungle\armadillo outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\armadillo outline.bmp") '##
            .LeftPart = New Point(89, 5)
            .BothParts = False
        End With
        Dim ArmadilloEye As New Part   'ok2
        With ArmadilloEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Armadillo"
            .FullImage = Image.FromFile(strPath & "\PackJungle\armadillo eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\armadillo eye.bmp") '##
            .LeftPart = New Point(152, 271)
            .RightPart = New Point(323, 271)
            .BothParts = True
        End With
        Dim ArmadilloNose As New Part   'ok2
        With ArmadilloNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Armadillo"
            .FullImage = Image.FromFile(strPath & "\PackJungle\armadillo nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\armadillo nose.bmp") '##
            .LeftPart = New Point(226, 488)
            .RightPart = New Point(259, 488)
            .BothParts = True
        End With
        Dim ArmadilloEar As New Part   'ok2
        With ArmadilloEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Armadillo Left"
            .FullImage = Image.FromFile(strPath & "\PackJungle\armadillo ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\armadillo ear.bmp") '##
            .LeftPart = New Point(89, 5)
            .BothParts = False
        End With
        Dim RhinoOutline As New Part   'ok2
        With RhinoOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Rhino"
            .FullImage = Image.FromFile(strPath & "\PackJungle\rhino outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\rhino outline.bmp") '##
            .LeftPart = New Point(37, 3)
            .BothParts = False
        End With
        Dim ArmadilloEarRight As New Part   'ok2
        With ArmadilloEarRight
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Armadillo Right"
            .FullImage = Image.FromFile(strPath & "\PackJungle\armadillo ear right.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\armadillo ear right.bmp") '##
            .LeftPart = New Point(304, 5)
            .BothParts = False
        End With
        Dim ChameleonOutline As New Part   'ok2
        With ChameleonOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Chameleon"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chameleon outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chameleon outline.bmp") '##
            .LeftPart = New Point(20, 7)
            .BothParts = False
        End With
        Dim ChameleonNose As New Part   'ok2
        With ChameleonNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Chameleon"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chameleon nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chameleon nose.bmp") '##
            .LeftPart = New Point(122, 197)
            .BothParts = False
        End With
        Dim ChameleonEye As New Part   'ok2
        With ChameleonEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Chameleon"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chameleon eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chameleon eye.bmp") '##
            .LeftPart = New Point(20, 167)
            .RightPart = New Point(376, 167)
            .BothParts = True
        End With
        Dim ChameleonMouth As New Part   'ok2
        With ChameleonMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Chameleon"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chameleon mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chameleon mouth.bmp") '##
            .LeftPart = New Point(50, 286)
            .BothParts = False
        End With
        Dim ChameleonBrow As New Part   'ok2
        With ChameleonBrow
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Chameleon Brow"
            .FullImage = Image.FromFile(strPath & "\PackJungle\chameleon brow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\chameleon brow.bmp") '##
            .LeftPart = New Point(216, 101)
            .BothParts = False
        End With
        Dim ToucanOutline As New Part   'ok2
        With ToucanOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Toucan"
            .FullImage = Image.FromFile(strPath & "\PackJungle\toucan outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\toucan outline.bmp") '##
            .LeftPart = New Point(66, -1)
            .BothParts = False
        End With
        Dim ToucanBeak As New Part   'ok2
        With ToucanBeak
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Toucan"
            .FullImage = Image.FromFile(strPath & "\PackJungle\toucan beak.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\toucan beak.bmp") '##
            .LeftPart = New Point(130, 58)
            .BothParts = False
        End With
        Dim ToucanEye As New Part   'ok2
        With ToucanEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Toucan"
            .FullImage = Image.FromFile(strPath & "\PackJungle\toucan eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\toucan eye.bmp") '##
            .LeftPart = New Point(97, 87)
            .RightPart = New Point(355, 87)
            .BothParts = True
        End With

        Dim SnakeOutline As New Part   'ok2
        With SnakeOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Snake"
            .FullImage = Image.FromFile(strPath & "\PackJungle\snake outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\snake outline.bmp") '##
            .LeftPart = New Point(0, 25)
            .BothParts = False
        End With
        Dim SnakeEye As New Part   'ok2
        With SnakeEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Snake"
            .FullImage = Image.FromFile(strPath & "\PackJungle\snake eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\snake eye.bmp") '##
            .LeftPart = New Point(146, 48)
            .RightPart = New Point(317, 48)
            .BothParts = True
        End With
        Dim SnakeFang As New Part   'ok2
        With SnakeFang
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Snake Fang"
            .FullImage = Image.FromFile(strPath & "\PackJungle\snake fang.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\snake fang.bmp") '##
            .LeftPart = New Point(168, 132)
            .RightPart = New Point(300, 132)
            .BothParts = True
        End With
        Dim SnakeTongue As New Part   'ok2
        With SnakeTongue
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Snake Tongue"
            .FullImage = Image.FromFile(strPath & "\PackJungle\snake tongue.png")
            .ThumbImage = Image.FromFile(strPath & "\PackJungle\snake tongue.bmp") '##
            .LeftPart = New Point(201, 184)
            .BothParts = False
        End With
        '
        Dim FPs As New FacePartStuctureDataFile
        With FPs
            .Parts.Add(RhinoOutline) '0
            .Parts.Add(RhinoEye) '    1
            .Parts.Add(RhinoEar) '	  2
            .Parts.Add(RhinoNose) '   3
            .Parts.Add(RhinoHorns) '  4

            .Parts.Add(ChimpOutline) '5
            .Parts.Add(ChimpEye) '	  6
            .Parts.Add(ChimpEar) '	  7
            .Parts.Add(ChimpNose) '	  8
            .Parts.Add(ChimpMouth) '  9
            .Parts.Add(ChimpChin) '	 10
            .Parts.Add(ChimpBrow) '	 11

            .Parts.Add(ParrotOutline) '12
            .Parts.Add(ParrotEye) '	   13
            .Parts.Add(ParrotMouth) '  14
            .Parts.Add(ParrotBeak) '   15

            .Parts.Add(PantherOutline) '16
            .Parts.Add(PantherEye) '	17
            .Parts.Add(PantherEar) '	18
            .Parts.Add(PantherNose) '	19
            .Parts.Add(PantherWhiskers) '20
            .Parts.Add(PantherBridge) '	 21
            .Parts.Add(PantherEyebrow) ' 22

            .Parts.Add(HippoOutline) '	 23
            .Parts.Add(HippoNose) '	     24
            .Parts.Add(HippoEar) '	     25
            .Parts.Add(HippoNose) '	     26

            .Parts.Add(GiraffeOutline) ' 27
            .Parts.Add(GiraffeEye) '	 28
            .Parts.Add(GiraffeEar) '	 29
            .Parts.Add(GiraffeNose) '	 30

            .Parts.Add(LionOutline) '	 31
            .Parts.Add(LionEye) '	     32
            .Parts.Add(LionEar) '	     33
            .Parts.Add(LionMouth) '	     34
            .Parts.Add(LionBrow) '	     35

            .Parts.Add(GorillaOutline) ' 36
            .Parts.Add(GorillaEye) '	 37
            .Parts.Add(GorillaNose) '	 38
            .Parts.Add(GorillaMouth) '	 39
            .Parts.Add(GorillaLines) '	 40
            .Parts.Add(GorillaBrow) '	 41

            .Parts.Add(ZebraOutline) '	 42
            .Parts.Add(ZebraEye) '	     43
            .Parts.Add(ZebraEar) '	     44
            .Parts.Add(ZebraNose) '	     45
            .Parts.Add(ZebraMane) '	     46

            .Parts.Add(CrocodileOutline) '47
            .Parts.Add(CrocodileEye) '	  48
            .Parts.Add(CrocodileTeeth) '  49
            .Parts.Add(CrocodileBrow) '	  50

            .Parts.Add(ElephantOutline) ' 51
            .Parts.Add(ElephantEye) '	  52
            .Parts.Add(ElephantEar) '	  53
            .Parts.Add(ElephantTusk) '	  54

            .Parts.Add(PorcupineOutline) '55
            .Parts.Add(PorcupineEye) '	  56
            .Parts.Add(PorcupineMouth) '  57
            .Parts.Add(PorcupineCheek) '  58
            .Parts.Add(PorcupineBrow) '	  59

            .Parts.Add(AnteaterOutline) ' 60
            .Parts.Add(AnteaterEye) '	  61
            .Parts.Add(AnteaterEar) '	  62
            .Parts.Add(AnteaterNose) '	  63

            .Parts.Add(ArmadilloOutline) '64
            .Parts.Add(ArmadilloEye) '	  65
            .Parts.Add(ArmadilloEar) '	  66
            .Parts.Add(ArmadilloNose) '	  67
            .Parts.Add(ArmadilloEarRight) '68

            .Parts.Add(ChameleonOutline) ' 69
            .Parts.Add(ChameleonEye) '     70
            .Parts.Add(ChameleonNose) '    71
            .Parts.Add(ChameleonMouth) '   72
            .Parts.Add(ChameleonBrow) '    73

            .Parts.Add(ToucanOutline) '    74
            .Parts.Add(ToucanEye) '        75
            .Parts.Add(ToucanBeak) '       76

            .Parts.Add(SnakeOutline) '     77
            .Parts.Add(SnakeEye) '         78
            .Parts.Add(SnakeFang) '        79
            .Parts.Add(SnakeTongue) '      80

            .Description = "Jungle"
            .DescImage = Image.FromFile(strPath & "\PackJungle\logo.png")
            .ProductNumber = "223019"
            .VersionNum = "1.0"
        End With

        Return FPs

    End Function
End Module
