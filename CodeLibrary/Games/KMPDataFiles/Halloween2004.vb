Module Halloween2004
    Function Halloween2004() As FacePartStuctureDataFile

        Dim strPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName())

        Dim FrankEye As New Part
        With FrankEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Frank"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Frank eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Frank eye.bmp")
            .LeftPart = New Point(139, 224)
            .RightPart = New Point(265, 223)
            .BothParts = True
        End With
        Dim DevilEye As New Part
        With DevilEye
            .PartType = FacePartEnums.ePartType.Eye
            .FaceMaster = "Devil"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Devil eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Devil eye.bmp")
            .LeftPart = New Point(150, 178)
            .RightPart = New Point(252, 178)
            .BothParts = True
        End With
        Dim FrankEar As New Part
        With FrankEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Frank"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Frank ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Frank ear.bmp")
            .LeftPart = New Point(61, 220)
            .RightPart = New Point(359, 223)
            .BothParts = True
        End With
        Dim DevilEar As New Part
        With DevilEar
            .PartType = FacePartEnums.ePartType.Ear
            .FaceMaster = "Devil"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Devil ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Devil ear.bmp")
            .LeftPart = New Point(29, 79)
            .RightPart = New Point(415, 80)
            .BothParts = True
        End With
        Dim FrankMouth As New Part
        With FrankMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Frank"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Frank mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Frank mouth.bmp")
            .LeftPart = New Point(173, 380)
            .BothParts = False
        End With
        Dim DevilMouth As New Part
        With DevilMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FaceMaster = "Devil"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Devil mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Devil mouth.bmp")
            .LeftPart = New Point(167, 334)
            .BothParts = False
        End With
        Dim FrankNose As New Part
        With FrankNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Frank"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Frank nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Frank nose.bmp")
            .LeftPart = New Point(189, 260)
            .BothParts = False
        End With
        Dim DevilNose As New Part
        With DevilNose
            .PartType = FacePartEnums.ePartType.Nose
            .FaceMaster = "Devil"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Devil nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Devil nose.bmp")
            .LeftPart = New Point(202, 237)
            .BothParts = False
        End With
        Dim FrankOutline As New Part
        With FrankOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Frank"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Frank Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Frank Outline.bmp")
            .LeftPart = New Point(61, -2)
            .BothParts = False
        End With
        Dim DevilOutline As New Part
        With DevilOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FaceMaster = "Devil"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Devil Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Devil Outline.bmp")
            .LeftPart = New Point(29, -1)
            .BothParts = False
        End With
        Dim FrankHair As New Part
        With FrankHair
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Frank Hair"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Frank hair.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Frank hair.bmp")
            .LeftPart = New Point(108, 42)
            .BothParts = False
        End With
        Dim FrankChin As New Part
        With FrankChin
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Frank Chin"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Frank chin.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Frank chin.bmp")
            .LeftPart = New Point(204, 447)
            .BothParts = False
        End With
        Dim FrankBolt As New Part
        With FrankBolt
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Frank Bolt"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Frank bolt.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Frank bolt.bmp")
            .LeftPart = New Point(80, 367)
            .RightPart = New Point(375, 367)
            .BothParts = True
        End With
        Dim DevilHorn As New Part
        With DevilHorn
            .PartType = FacePartEnums.ePartType.Misc
            .FaceMaster = "Devil Horn"
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Devil horn.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Devil horn.bmp")
            .LeftPart = New Point(136, -1)
            .RightPart = New Point(301, -1)
            .BothParts = True
        End With

        Dim AlienNose As New Part   'two
        With AlienNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Alien nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Alien nose.bmp")
            .FaceMaster = "Alien"
            .LeftPart = New Point(234, 325)
            .BothParts = False
        End With
        Dim AlienMouth As New Part   'two
        With AlienMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Alien mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Alien mouth.bmp")
            .FaceMaster = "Alien"
            .LeftPart = New Point(191, 385)
            .BothParts = False
        End With
        Dim AlienEye As New Part   'two
        With AlienEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Alien eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Alien eye.bmp")
            .FaceMaster = "Alien"
            .LeftPart = New Point(74, 167)
            .RightPart = New Point(262, 167)
            .BothParts = True
        End With
        Dim AlienFace As New Part   'two
        With AlienFace
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Alien face.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Alien face.bmp")
            .FaceMaster = "Alien"
            .LeftPart = New Point(58, 1)
            .BothParts = False
        End With
        Dim HockeyOutline As New Part   'two
        With HockeyOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Hockey Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Hockey Outline.bmp")
            .FaceMaster = "Hockey"
            .LeftPart = New Point(69, 0)
            .BothParts = False
        End With
        Dim HockeyEye As New Part   'two
        With HockeyEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Hockey eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Hockey eye.bmp")
            .FaceMaster = "Hockey"
            .LeftPart = New Point(126, 175)
            .RightPart = New Point(284, 175)
            .BothParts = True
        End With
        Dim HockeyHoles As New Part   'two
        With HockeyHoles
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Hockey holes.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Hockey holes.bmp")
            .FaceMaster = "Hockey Holes"
            .LeftPart = New Point(206, 46)
            .BothParts = False
        End With
        Dim HockeyMouth As New Part   'two
        With HockeyMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Hockey mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Hockey mouth.bmp")
            .FaceMaster = "Hockey"
            .LeftPart = New Point(188, 343)
            .BothParts = False
        End With
        Dim MummyOutline As New Part   'two
        With MummyOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Mummy Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Mummy Outline.bmp")
            .FaceMaster = "Mummy"
            .LeftPart = New Point(47, 0)
            .BothParts = False
        End With
        Dim MummyEye As New Part   'two
        With MummyEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Mummy eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Mummy eye.bmp")
            .FaceMaster = "Mummy"
            .LeftPart = New Point(130, 192)
            .RightPart = New Point(278, 192)
            .BothParts = True
        End With
        Dim MummyMouth As New Part   'two
        With MummyMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Mummy mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Mummy mouth.bmp")
            .FaceMaster = "Mummy"
            .LeftPart = New Point(161, 375)
            .BothParts = False
        End With
        Dim PumpkinOutline As New Part   'two
        With PumpkinOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Pumpkin Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Pumpkin Outline.bmp")
            .FaceMaster = "Pumpkin"
            .LeftPart = New Point(45, 1)
            .BothParts = False
        End With
        Dim PumpkinNose As New Part   'two
        With PumpkinNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Pumpkin nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Pumpkin nose.bmp")
            .FaceMaster = "Pumpkin"
            .LeftPart = New Point(210, 301)
            .BothParts = False
        End With
        Dim PumpkinMouth As New Part   'two
        With PumpkinMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Pumpkin mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Pumpkin mouth.bmp")
            .FaceMaster = "Pumpkin"
            .LeftPart = New Point(103, 374)
            .BothParts = False
        End With
        Dim PumpkinEye As New Part   'two
        With PumpkinEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Pumpkin eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Pumpkin eye.bmp")
            .FaceMaster = "Pumpkin"
            .LeftPart = New Point(108, 203)
            .RightPart = New Point(296, 203)
            .BothParts = True
        End With
        Dim SkullMouth As New Part   'two
        With SkullMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Skull mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Skull mouth.bmp")
            .FaceMaster = "Skull"
            .LeftPart = New Point(105, 281)
            .BothParts = False
        End With
        Dim SkullEye As New Part   'two
        With SkullEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Skull eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Skull eye.bmp")
            .FaceMaster = "Skull"
            .LeftPart = New Point(50, 66)
            .RightPart = New Point(254, 66)
            .BothParts = True
        End With
        Dim SkullOutline As New Part   'two
        With SkullOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Skull Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Skull Outline.bmp")
            .FaceMaster = "Skull"
            .LeftPart = New Point(50, -2)
            .BothParts = False
        End With
        Dim SkullNose As New Part   'two
        With SkullNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Skull nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Skull nose.bmp")
            .FaceMaster = "Skull"
            .LeftPart = New Point(200, 221)
            .BothParts = False
        End With
        Dim VampireOutline As New Part   'two
        With VampireOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire Outline.bmp")
            .FaceMaster = "Vampire"
            .LeftPart = New Point(5, 2)
            .BothParts = False
        End With
        Dim VampireHair As New Part   'two
        With VampireHair
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire hair.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire hair.bmp")
            .FaceMaster = "Vampire Hair"
            .LeftPart = New Point(92, 2)
            .BothParts = False
        End With
        Dim VampireNose As New Part   'two
        With VampireNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire nose.bmp")
            .FaceMaster = "Vampire"
            .LeftPart = New Point(180, 230)
            .BothParts = False
        End With
        Dim VampireTeeth As New Part   'two
        With VampireTeeth
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire teeth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire teeth.bmp")
            .FaceMaster = "Vampire Teeth"
            .LeftPart = New Point(190, 371)
            .BothParts = False
        End With
        Dim VampireEye As New Part   'two
        With VampireEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire eye.bmp")
            .FaceMaster = "Vampire"
            .LeftPart = New Point(122, 194)
            .RightPart = New Point(269, 194)
            .BothParts = True
        End With
        Dim VampireEar As New Part   'two
        With VampireEar
            .PartType = FacePartEnums.ePartType.Ear
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Vampire ear.bmp")
            .FaceMaster = "Vampire"
            .LeftPart = New Point(5, 196)
            .RightPart = New Point(388, 196)
            .BothParts = True
        End With
        Dim WerewolfNose As New Part   'two
        With WerewolfNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf nose.bmp")
            .FaceMaster = "Werewolf"
            .LeftPart = New Point(212, 291)
            .BothParts = False
        End With
        Dim WerewolfEar As New Part   'two
        With WerewolfEar
            .PartType = FacePartEnums.ePartType.Ear
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf ear.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf ear.bmp")
            .FaceMaster = "Werewolf"
            .LeftPart = New Point(-4, 118)
            .RightPart = New Point(417, 118)
            .BothParts = True
        End With
        Dim WerewolfMouth As New Part   'two
        With WerewolfMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf mouth.bmp")
            .FaceMaster = "Werewolf"
            .LeftPart = New Point(174, 328)
            .BothParts = False
        End With
        Dim WerewolfEye As New Part   'two
        With WerewolfEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf eye.bmp")
            .FaceMaster = "Werewolf"
            .LeftPart = New Point(158, 244)
            .RightPart = New Point(292, 244)
            .BothParts = True
        End With
        Dim WerewolfEyebrow As New Part   'two
        With WerewolfEyebrow
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf Eyebrow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf Eyebrow.bmp")
            .FaceMaster = "Werewolf Eyebrow"
            .LeftPart = New Point(88, 118)
            .RightPart = New Point(254, 118)
            .BothParts = True
        End With
        Dim WerewolfOutline As New Part   'two
        With WerewolfOutline
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf  Outline.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Werewolf  Outline.bmp")
            .FaceMaster = "Werewolf"
            .LeftPart = New Point(-4, 17)
            .BothParts = False
        End With
        Dim WitchCheekBones As New Part   'two
        With WitchCheekBones
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Witch cheek bones.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Witch cheek bones.bmp")
            .FaceMaster = "Witch Cheek Bones"
            .LeftPart = New Point(154, 276)
            .RightPart = New Point(289, 276)
            .BothParts = True
        End With
        Dim WitchEyebrow As New Part   'two
        With WitchEyebrow
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Witch eye brow.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Witch eye brow.bmp")
            .FaceMaster = "Witch Eyebrow"
            .LeftPart = New Point(136, 190)
            .RightPart = New Point(276, 190)
            .BothParts = True
        End With
        Dim WitchEye As New Part   'two
        With WitchEye
            .PartType = FacePartEnums.ePartType.Eye
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Witch eye.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Witch eye.bmp")
            .FaceMaster = "Witch"
            .LeftPart = New Point(152, 205)
            .RightPart = New Point(270, 205)
            .BothParts = True
        End With
        Dim WitchMouth As New Part   'two
        With WitchMouth
            .PartType = FacePartEnums.ePartType.Mouth
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Witch mouth.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Witch mouth.bmp")
            .FaceMaster = "Witch"
            .LeftPart = New Point(187, 352)
            .BothParts = False
        End With
        Dim WitchNose As New Part   'two
        With WitchNose
            .PartType = FacePartEnums.ePartType.Nose
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Witch nose.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Witch nose.bmp")
            .FaceMaster = "Witch"
            .LeftPart = New Point(217, 276)
            .BothParts = False
        End With
        Dim WitchHat As New Part   'two
        With WitchHat
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Witch hat.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Witch hat.bmp")
            .FaceMaster = "Witch Hat"
            .LeftPart = New Point(55, -1)
            .BothParts = False
        End With
        Dim WitchHairRight As New Part   'two
        With WitchHairRight
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Witch hair right.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Witch hair right.bmp")
            .FaceMaster = "Witch Hair Right"
            .LeftPart = New Point(274, 210)
            .BothParts = False
        End With
        Dim WitchHairLeft As New Part   'two
        With WitchHairLeft
            .PartType = FacePartEnums.ePartType.Misc
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Witch hair left.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Witch hair left.bmp")
            .FaceMaster = "Witch Hair Left"
            .LeftPart = New Point(45, 191)
            .BothParts = False
        End With
        Dim WitchFace As New Part   'two
        With WitchFace
            .PartType = FacePartEnums.ePartType.Outline
            .FullImage = Image.FromFile(strPath & "\PackHalloween2004\Witch face.png")
            .ThumbImage = Image.FromFile(strPath & "\PackHalloween2004\Witch face.bmp")
            .FaceMaster = "Witch"
            .LeftPart = New Point(112, 156)
            .BothParts = False
        End With

        Dim FPs As New FacePartStuctureDataFile
        With FPs
            .Parts.Add(FrankOutline) '0
            .Parts.Add(FrankEye) '1
            .Parts.Add(FrankEar) '2
            .Parts.Add(FrankMouth) '3
            .Parts.Add(FrankNose) '4
            .Parts.Add(FrankHair) '5
            .Parts.Add(FrankChin) '6
            .Parts.Add(FrankBolt) '7

            .Parts.Add(DevilOutline) '8
            .Parts.Add(DevilNose) '9
            .Parts.Add(DevilMouth) '10
            .Parts.Add(DevilEar) '11
            .Parts.Add(DevilEye) '12
            .Parts.Add(DevilHorn) '13

            .Parts.Add(AlienFace) '14
            .Parts.Add(AlienNose) '15
            .Parts.Add(AlienMouth) '16
            .Parts.Add(AlienEye) '17

            .Parts.Add(HockeyOutline) '18
            .Parts.Add(HockeyEye) '19
            .Parts.Add(HockeyHoles) '20
            .Parts.Add(HockeyMouth) '21

            .Parts.Add(MummyOutline) '22
            .Parts.Add(MummyEye) '23
            .Parts.Add(MummyMouth) '24

            .Parts.Add(PumpkinOutline) '25
            .Parts.Add(PumpkinNose) '26
            .Parts.Add(PumpkinMouth) '27
            .Parts.Add(PumpkinEye) '28

            .Parts.Add(SkullOutline) '29
            .Parts.Add(SkullMouth) '30
            .Parts.Add(SkullEye) '31
            .Parts.Add(SkullNose) '32

            .Parts.Add(VampireOutline) '33
            .Parts.Add(VampireHair) '34
            .Parts.Add(VampireNose) '35
            .Parts.Add(VampireTeeth) '36
            .Parts.Add(VampireEye) '37
            .Parts.Add(VampireEar) '38

            .Parts.Add(WerewolfOutline) '39
            .Parts.Add(WerewolfNose) '40
            .Parts.Add(WerewolfEar) '41
            .Parts.Add(WerewolfMouth) '42
            .Parts.Add(WerewolfEye) '43
            .Parts.Add(WerewolfEyebrow) '44

            .Parts.Add(WitchFace) '45
            .Parts.Add(WitchCheekBones) '46
            .Parts.Add(WitchEyebrow) '47
            .Parts.Add(WitchEye) '48
            .Parts.Add(WitchMouth) '49
            .Parts.Add(WitchNose) '50
            .Parts.Add(WitchHat) '51
            .Parts.Add(WitchHairRight) '52
            .Parts.Add(WitchHairLeft) '53

            .Description = "Halloween" 'Halloween2004
            .DescImage = Image.FromFile(strPath & "\PackHalloween2004\logo.png")
            .ProductNumber = "223018" '"KM-00101U"
            .VersionNum = "1.0"
        End With

        Return FPs

    End Function
End Module
