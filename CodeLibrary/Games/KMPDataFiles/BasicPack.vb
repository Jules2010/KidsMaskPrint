Module BasicPack
    Function BasicPack() As FacePartStuctureDataFile

        Dim strPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName())

        Dim OgreEye As New Part()
        With OgreEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Ogre"
            .FullImage = Image.FromFile(strPath & "\PackBasic\ogre eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\ogre eye.bmp")
            .LeftPart = New Point(132 + 10, 152)
            .RightPart = New Point(258 + 10, 152)
            .BothParts = True
        End With
        Dim HarlequinEye As New Part
        With HarlequinEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Harlequin"
            .FullImage = Image.FromFile(strPath & "\PackBasic\harliquin eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\harliquin eye.bmp")
            .LeftPart = New Point(142, 238)
            .RightPart = New Point(275, 238)
            .BothParts = True
        End With
        Dim GoblinEye As New Part
        With GoblinEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Goblin"
            .FullImage = Image.FromFile(strPath & "\PackBasic\gollum eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\gollum eye.bmp")
            .LeftPart = New Point(84, 226)
            .RightPart = New Point(283, 226)
            .BothParts = True
        End With
        Dim OgreEar As New Part
        With OgreEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Ogre"
            .FullImage = Image.FromFile(strPath & "\PackBasic\ogre ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\ogre ear.bmp")
            .LeftPart = New Point(-14 + 10, 109)
            .RightPart = New Point(386 + 10, 110)
            .BothParts = True
        End With

        Dim GoblinEar As New Part
        With GoblinEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Goblin"
            .FullImage = Image.FromFile(strPath & "\PackBasic\gollum ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\gollum ear.bmp")
            .LeftPart = New Point(13, 193)
            .RightPart = New Point(423, 193)
            .BothParts = True
        End With
        Dim OgreMouth As New Part
        With OgreMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Ogre"
            .FullImage = Image.FromFile(strPath & "\PackBasic\ogre mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\ogre mouth.bmp")
            .LeftPart = New Point(126 + 10, 292)
            .BothParts = False
        End With
        Dim HarlequinMouth As New Part
        With HarlequinMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Harlequin"
            .FullImage = Image.FromFile(strPath & "\PackBasic\harliquin mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\harliquin mouth.bmp")
            .LeftPart = New Point(165, 340)
            .BothParts = False
        End With
        Dim GoblinMouth As New Part
        With GoblinMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Goblin"
            .FullImage = Image.FromFile(strPath & "\PackBasic\gollum mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\gollum mouth.bmp")
            .LeftPart = New Point(169, 373)
            .BothParts = False
        End With
        Dim OgreNose As New Part
        With OgreNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Ogre"
            .FullImage = Image.FromFile(strPath & "\PackBasic\ogre nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\ogre nose.bmp")
            .LeftPart = New Point(171 + 10, 188)
            .BothParts = False
        End With

        Dim HarlequinNose As New Part
        With HarlequinNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Harlequin"
            .FullImage = Image.FromFile(strPath & "\PackBasic\harliquin nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\harliquin nose.bmp")
            .LeftPart = New Point(191, 238)
            .BothParts = False
        End With
        Dim GoblinNose As New Part
        With GoblinNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Goblin"
            .FullImage = Image.FromFile(strPath & "\PackBasic\gollum nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\gollum nose.bmp")
            .LeftPart = New Point(204, 257)
            .BothParts = False
        End With
        Dim OgreOutline As New Part
        With OgreOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Ogre"
            .FullImage = Image.FromFile(strPath & "\PackBasic\ogre Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\ogre Outline.bmp")
            .LeftPart = New Point(-8 + 10, 47)
            .BothParts = False
        End With
        Dim GoblinOutline As New Part
        With GoblinOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Goblin"
            .FullImage = Image.FromFile(strPath & "\PackBasic\gollum Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\gollum Outline.bmp")
            .LeftPart = New Point(13, -1)
            .BothParts = False
        End With
        Dim OgreEyebrow As New Part
        With OgreEyebrow
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Ogre Eyebrow"
            .FullImage = Image.FromFile(strPath & "\PackBasic\ogre Eyebrow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\ogre Eyebrow.bmp")
            .LeftPart = New Point(133 + 10, 93)
            .RightPart = New Point(254 + 10, 93)
            .BothParts = True
        End With

        Dim HarlequinEyebrow As New Part
        With HarlequinEyebrow
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Harlequin Eyebrow"
            .FullImage = Image.FromFile(strPath & "\PackBasic\harliquin Eyebrow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\harliquin Eyebrow.bmp")
            .LeftPart = New Point(137, 197)
            .RightPart = New Point(258, 197)
            .BothParts = True
        End With


        Dim PirateRightEar As New Part   'two
        With PirateRightEar
            .PartType = FacePartEnums.ePartType.Ear
            .FullImage = Image.FromFile(strPath & "\PackBasic\Pirate right ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Pirate right ear.bmp")
            .FaceMaster = "Pirate Right"
            .LeftPart = New Point(406, 150)
            .BothParts = False
        End With
        Dim PirateEyePatch As New Part   'two
        With PirateEyePatch
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\Pirate eye patch.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Pirate eye patch.bmp")
            .FaceMaster = "Pirate Eye Patch"
            .LeftPart = New Point(105, 143)
            .BothParts = False
        End With
        Dim PirateLeftEyebrow As New Part   'two
        With PirateLeftEyebrow
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\Pirate left Eyebrow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Pirate left Eyebrow.bmp")
            .FaceMaster = "Pirate Left Eyebrow"
            .LeftPart = New Point(101, 154)
            .BothParts = False
        End With
        Dim PirateRightEyebrow As New Part   'two
        With PirateRightEyebrow
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\Pirate right Eyebrow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Pirate right Eyebrow.bmp")
            .FaceMaster = "Pirate Right Eyebrow"
            .LeftPart = New Point(271, 111)
            .BothParts = False
        End With
        Dim PirateNose As New Part   'two
        With PirateNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackBasic\Pirate nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Pirate nose.bmp")
            .FaceMaster = "Pirate"
            .LeftPart = New Point(155, 294)
            .BothParts = False
        End With
        Dim PirateMoustache As New Part   'two
        With PirateMoustache
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\Pirate moustache.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Pirate moustache.bmp")
            .FaceMaster = "Pirate Moustache"
            .LeftPart = New Point(6, 312)
            .BothParts = False
        End With
        Dim PirateFace As New Part   'two
        With PirateFace
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackBasic\Pirate face.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Pirate face.bmp")
            .FaceMaster = "Pirate"
            .LeftPart = New Point(20, -1)
            .BothParts = False
        End With
        Dim CatOutline As New Part   'two
        With CatOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackBasic\Cat Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Cat Outline.bmp")
            .FaceMaster = "Cat"
            .LeftPart = New Point(21, -3)
            .BothParts = False
        End With
        Dim CatNose As New Part   'two
        With CatNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackBasic\Cat nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Cat nose.bmp")
            .FaceMaster = "Cat"
            .LeftPart = New Point(208, 226)
            .BothParts = False
        End With
        Dim CatEye As New Part   'two
        With CatEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackBasic\Cat eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Cat eye.bmp")
            .FaceMaster = "Cat"
            .LeftPart = New Point(119, 179)
            .RightPart = New Point(278, 179)
            .BothParts = True
        End With
        Dim CatMouth As New Part   'two
        With CatMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackBasic\Cat mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Cat mouth.bmp")
            .FaceMaster = "Cat"
            .LeftPart = New Point(57, 277)
            .BothParts = False
        End With
        Dim CatEar As New Part   'two
        With CatEar
            .PartType = FacePartEnums.ePartType.Ear
            .FullImage = Image.FromFile(strPath & "\PackBasic\Cat ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Cat ear.bmp")
            .FaceMaster = "Cat"
            .LeftPart = New Point(21, -6)
            .RightPart = New Point(337, -7)
            .BothParts = True
        End With
        Dim BurglarHat As New Part   'two
        With BurglarHat
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\Burglar hat.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Burglar hat.bmp")
            .FaceMaster = "Burglar Hat"
            .LeftPart = New Point(31, -1)
            .BothParts = False
        End With
        Dim BurglarFace As New Part   'two
        With BurglarFace
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackBasic\Burglar face.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Burglar face.bmp")
            .FaceMaster = "Burglar Face"
            .LeftPart = New Point(102, 331)
            .BothParts = False
        End With
        Dim BurglarEar As New Part   'two
        With BurglarEar
            .PartType = FacePartEnums.ePartType.Ear
            .FullImage = Image.FromFile(strPath & "\PackBasic\Burglar ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Burglar ear.bmp")
            .FaceMaster = "Burglar"
            .LeftPart = New Point(22, 224)
            .RightPart = New Point(389, 224)
            .BothParts = True
        End With
        Dim BurglarNose As New Part   'two
        With BurglarNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackBasic\Burglar nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Burglar nose.bmp")
            .FaceMaster = "Burglar"
            .LeftPart = New Point(174, 305)
            .BothParts = False
        End With
        Dim BurglarMask As New Part   'two
        With BurglarMask
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\Burglar mask.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Burglar mask.bmp")
            .FaceMaster = "Burglar Mask"
            .LeftPart = New Point(97, 205)
            .BothParts = False
        End With
        Dim BurglarEye As New Part   'two
        With BurglarEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackBasic\Burglar eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Burglar eye.bmp")
            .FaceMaster = "Burglar"
            .LeftPart = New Point(118, 238)
            .RightPart = New Point(266, 238)
            .BothParts = True
        End With
        Dim BurglarMouth As New Part   'two
        With BurglarMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackBasic\Burglar mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Burglar mouth.bmp")
            .FaceMaster = "Burglar Mouth"
            .LeftPart = New Point(190, 377)
            .BothParts = False
        End With
        Dim PigNose As New Part   'two
        With PigNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackBasic\pig nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\pig nose.bmp")
            .FaceMaster = "Pig"
            .LeftPart = New Point(163, 236)
            .BothParts = False
        End With
        Dim PigEye As New Part   'two
        With PigEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackBasic\pig eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\pig eye.bmp")
            .FaceMaster = "Pig"
            .LeftPart = New Point(84, 180)
            .RightPart = New Point(298, 180)
            .BothParts = True
        End With
        Dim PigEar As New Part   'two
        With PigEar
            .PartType = FacePartEnums.ePartType.Ear
            .FullImage = Image.FromFile(strPath & "\PackBasic\pig ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\pig ear.bmp")
            .FaceMaster = "Pig"
            .LeftPart = New Point(39, 0)
            .RightPart = New Point(323, 0)
            .BothParts = True
        End With
        Dim PigMouth As New Part   'two
        With PigMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackBasic\pig mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\pig mouth.bmp")
            .FaceMaster = "Pig"
            .LeftPart = New Point(110, 331)
            .BothParts = False
        End With
        Dim PigFace As New Part   'two
        With PigFace
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackBasic\pig face.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\pig face.bmp")
            .FaceMaster = "Pig"
            .LeftPart = New Point(39, 49)
            .BothParts = False
        End With
        Dim TigerEar As New Part   'two
        With TigerEar
            .PartType = FacePartEnums.ePartType.Ear
            .FullImage = Image.FromFile(strPath & "\PackBasic\Tiger ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Tiger ear.bmp")
            .FaceMaster = "Tiger ear"
            .LeftPart = New Point(21, 1)
            .RightPart = New Point(349, 1)
            .BothParts = True
        End With
        Dim TigerFace As New Part   'two
        With TigerFace
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackBasic\Tiger face.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Tiger face.bmp")
            .FaceMaster = "Tiger"
            .LeftPart = New Point(21, 1)
            .BothParts = False
        End With
        Dim TigerEye As New Part   'two
        With TigerEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackBasic\Tiger eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Tiger eye.bmp")
            .FaceMaster = "Tiger"
            .LeftPart = New Point(107, 167)
            .RightPart = New Point(306, 167)
            .BothParts = True
        End With
        Dim TigerMouth As New Part   'two
        With TigerMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackBasic\Tiger mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Tiger mouth.bmp")
            .FaceMaster = "Tiger"
            .LeftPart = New Point(212, 375)
            .BothParts = False
        End With
        Dim TigerNose As New Part   'two
        With TigerNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackBasic\Tiger nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Tiger nose.bmp")
            .FaceMaster = "Tiger"
            .LeftPart = New Point(132, 278)
            .BothParts = False
        End With
        Dim SantaBeard As New Part   'two
        With SantaBeard
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\Santa beard.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Santa beard.bmp")
            .FaceMaster = "Santa Beard"
            .LeftPart = New Point(87, 204)
            .BothParts = False
        End With
        Dim SantaHat As New Part   'two
        With SantaHat
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\Santa hat.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Santa hat.bmp")
            .FaceMaster = "Santa Hat"
            .LeftPart = New Point(62, 0)
            .BothParts = False
        End With
        Dim SantaEye As New Part   'two
        With SantaEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackBasic\Santa eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Santa eye.bmp")
            .FaceMaster = "Santa"
            .LeftPart = New Point(183, 250)
            .RightPart = New Point(274, 250)
            .BothParts = True
        End With
        Dim SantaNose As New Part   'two
        With SantaNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackBasic\Santa nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Santa nose.bmp")
            .FaceMaster = "Santa"
            .LeftPart = New Point(210, 201)
            .BothParts = False
        End With
        Dim SantaMouth As New Part   'two
        With SantaMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackBasic\Santa mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Santa mouth.bmp")
            .FaceMaster = "Santa"
            .LeftPart = New Point(170, 334)
            .BothParts = False
        End With
        Dim SantaEyebrow As New Part   'two
        With SantaEyebrow
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\Santa Eyebrow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Santa Eyebrow.bmp")
            .FaceMaster = "Santa Eyebrow"
            .LeftPart = New Point(171, 182)
            .RightPart = New Point(273, 182)
            .BothParts = True
        End With
        Dim EvilClownEye As New Part   'two
        With EvilClownEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackBasic\Clown eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Clown eye.bmp")
            .FaceMaster = "Evil Clown"
            .LeftPart = New Point(155, 215)
            .RightPart = New Point(271, 215)
            .BothParts = True
        End With
        Dim EvilClownNose As New Part   'two
        With EvilClownNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackBasic\Clown nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Clown nose.bmp")
            .FaceMaster = "Evil Clown"
            .LeftPart = New Point(146, 281)
            .BothParts = False
        End With
        Dim EvilClownBrow As New Part   'two
        With EvilClownBrow
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\Clown brow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Clown brow.bmp")
            .FaceMaster = "Evil Clown Brow"
            .LeftPart = New Point(147, 120)
            .BothParts = False
        End With
        Dim EvilClownMouth As New Part   'two
        With EvilClownMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackBasic\Clown mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Clown mouth.bmp")
            .FaceMaster = "Evil Clown"
            .LeftPart = New Point(201, 386)
            .BothParts = False
        End With
        Dim EvilClownOutline As New Part   'two
        With EvilClownOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackBasic\Clown Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Clown Outline.bmp")
            .FaceMaster = "Evil Clown"
            .LeftPart = New Point(13, 0)
            .BothParts = False
        End With
        Dim PirateLeftEar As New Part   'two
        With PirateLeftEar
            .PartType = FacePartEnums.ePartType.Ear
            .FullImage = Image.FromFile(strPath & "\PackBasic\Pirate left ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Pirate left ear.bmp")
            .FaceMaster = "Pirate Left"
            .LeftPart = New Point(11, 186)
            .BothParts = False
        End With
        Dim PirateEye As New Part   'two
        With PirateEye 'left eye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackBasic\Pirate eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\Pirate eye.bmp")
            .FaceMaster = "Pirate"
            .LeftPart = New Point(88, 230)
            .BothParts = False
        End With
        Dim HarlequinFace As New Part
        With HarlequinFace
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackBasic\harliquin face.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\harliquin face.bmp")
            .FaceMaster = "Harlequin"
            .LeftPart = New Point(119, 181)
            .BothParts = False
        End With
        Dim HarlequinHat As New Part
        With HarlequinHat
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\harliquin hat.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\harliquin hat.bmp")
            .FaceMaster = "Harlequin"
            .LeftPart = New Point(4, 3)
            .BothParts = False
        End With
        Dim PirateBandanna As New Part
        With PirateBandanna
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\pirate bandanna.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\pirate bandanna.bmp")
            .FaceMaster = "Pirate Bandanna"
            .LeftPart = New Point(20, -1)
            .BothParts = False
        End With
        Dim ClownFace As New Part
        With ClownFace
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackBasic\nice clown face.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\nice clown face.bmp")
            .FaceMaster = "Clown"
            .LeftPart = New Point(34, 6)
            .BothParts = False
        End With
        Dim ClownFlower As New Part
        With ClownFlower
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\nice clown flower.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\nice clown flower.bmp")
            .FaceMaster = "Clown Flower"
            .LeftPart = New Point(227, 20)
            .BothParts = False
        End With
        Dim ClownHair As New Part
        With ClownHair
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\nice clown hair.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\nice clown hair.bmp")
            .FaceMaster = "Clown Hair"
            .LeftPart = New Point(34, 117)
            .BothParts = False
        End With
        Dim ClownMouth As New Part
        With ClownMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackBasic\nice clown mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\nice clown mouth.bmp")
            .FaceMaster = "Clown"
            .LeftPart = New Point(158, 332)
            .BothParts = False
        End With
        Dim ClownNose As New Part
        With ClownNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackBasic\nice clown nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\nice clown nose.bmp")
            .FaceMaster = "Clown"
            .LeftPart = New Point(214, 297)
            .BothParts = False
        End With
        Dim ClownEye As New Part
        With ClownEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackBasic\nise clown eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\nise clown eye.bmp")
            .FaceMaster = "Clown"
            .LeftPart = New Point(148, 193)
            .RightPart = New Point(257, 193)
            .BothParts = True
        End With
        Dim ClownHat As New Part
        With ClownHat
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackBasic\nice clown hat.png")
            .ThumbImage = Image.FromFile(strPath & "\PackBasic\nice clown hat.bmp")
            .FaceMaster = "Clown Hat"
            .LeftPart = New Point(67, 6)
            .BothParts = False
        End With


        Dim FPs As New FacePartStuctureDataFile
        With FPs
            .Parts.Add(OgreOutline) '0
            .Parts.Add(OgreEye) '1
            .Parts.Add(OgreEar) '2
            .Parts.Add(OgreMouth) '3
            .Parts.Add(OgreNose) '4
            .Parts.Add(OgreEyebrow) '5

            .Parts.Add(HarlequinFace) '6
            '.Parts.Add(HarlequinOutline) '12
            .Parts.Add(HarlequinEye) '7
            .Parts.Add(HarlequinMouth) '8
            .Parts.Add(HarlequinNose) '9
            .Parts.Add(HarlequinEyebrow) '10
            .Parts.Add(HarlequinHat) '11

            .Parts.Add(GoblinOutline) '12
            .Parts.Add(GoblinEye) '13
            .Parts.Add(GoblinEar) '14
            .Parts.Add(GoblinMouth) '15
            .Parts.Add(GoblinNose) '16


            .Parts.Add(PirateFace) '17
            .Parts.Add(PirateLeftEar) '18
            .Parts.Add(PirateEye) '19
            .Parts.Add(PirateRightEar) '20
            .Parts.Add(PirateEyePatch) '21
            .Parts.Add(PirateLeftEyebrow) '22
            .Parts.Add(PirateRightEyebrow) '23
            .Parts.Add(PirateNose) '24
            .Parts.Add(PirateMoustache) '25
            .Parts.Add(PirateBandanna) ' 26

            .Parts.Add(CatOutline) '27
            .Parts.Add(CatNose) '28
            .Parts.Add(CatEye) '29
            .Parts.Add(CatMouth) '26
            .Parts.Add(CatEar) '27

            .Parts.Add(BurglarFace) '28
            .Parts.Add(BurglarHat) '29
            .Parts.Add(BurglarEar) '30
            .Parts.Add(BurglarNose) '31
            .Parts.Add(BurglarMask) '32
            .Parts.Add(BurglarEye) '33
            .Parts.Add(BurglarMouth) '34

            .Parts.Add(PigFace) '35
            .Parts.Add(PigNose) '36
            .Parts.Add(PigEye) '37
            .Parts.Add(PigEar) '38
            .Parts.Add(PigMouth) '39

            .Parts.Add(TigerFace) '40
            .Parts.Add(TigerEar) '41
            .Parts.Add(TigerEye) '42
            .Parts.Add(TigerMouth) '43
            .Parts.Add(TigerNose) '44

            .Parts.Add(SantaBeard) '45
            .Parts.Add(SantaHat) '46
            .Parts.Add(SantaEye) '47
            .Parts.Add(SantaNose) '48
            .Parts.Add(SantaMouth) '49
            .Parts.Add(SantaEyebrow) '50

            .Parts.Add(EvilClownOutline) '51
            .Parts.Add(EvilClownEye) '52
            .Parts.Add(EvilClownNose) '53
            .Parts.Add(EvilClownBrow) '54
            .Parts.Add(EvilClownMouth) '55

            .Parts.Add(ClownFace) '56
            .Parts.Add(ClownEye) '57
            .Parts.Add(ClownNose) '58
            .Parts.Add(ClownMouth) '59
            .Parts.Add(ClownHat) '60
            .Parts.Add(ClownFlower) '61
            .Parts.Add(ClownHair) '62


            .Description = "Basic Pack"
            .DescImage = Image.FromFile(strPath & "\PackHalloween2004\logo.png")
            .ProductNumber = "999999" '"KM-00101U"
            .VersionNum = "1.0"
        End With

        Return FPs

    End Function
End Module
