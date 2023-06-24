using System;
using System.Numerics;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

using ImGuiNET;
using System.Runtime.CompilerServices;

namespace imnodesNET
{
    using ImNodesAttributeType = Int32;
    using ImNodesClickInteractionType = Int32;

    using ImVec2 = Vector2;
    using ImRect = Vector4;

    using ImNodesMiniMapNodeHoveringCallbackUserData = Object;

    using ImU32 = UInt32;

    public unsafe delegate void ImNodesMiniMapNodeHoveringCallback(int i, void* p);

    public enum ImNodesScope
    {
        ImNodesScope_None = 1,
        ImNodesScope_Editor = 1 << 1,
        ImNodesScope_Node = 1 << 2,
        ImNodesScope_Attribute = 1 << 3
    }

    public unsafe struct ImNodesColElement
    {
        ImU32 Color;
        ImNodesCol Item;
    }

    public unsafe struct ImObjectPool<T>
    {
        ImVector<T> Pool;
        ImVector<bool> InUse;
        ImVector<int> FreeList;
        ImGuiStorage IdMap;
    };

    public unsafe struct ImNodesStyleVarElement
    {
        ImNodesStyleVar Item;
        float* FloatValue;
    }

    struct ImClickInteractionState
    {
        ImNodesClickInteractionType Type;
    }

        struct ImNodeData
    {
        int Id;
        ImVec2 Origin; // The node origin is in editor space
        ImRect TitleBarContentRect;
        ImRect Rect;
    }

    struct ImPinData
    {
        int Id;
        int ParentNodeIdx;
        ImRect AttributeRect;
        ImNodesAttributeType Type;
        ImNodesPinShape Shape;
        ImVec2 Pos; // screen-space coordinates
        int Flags;
    }

    struct ImLinkData
    {
        int Id;
        int StartPinIdx, EndPinIdx;
    }

    public unsafe struct ImNodesContextPtr
    {
        public ImNodesContext* NativePtr { get; }
        public ImNodesContextPtr(ImNodesContext* nativePtr) => NativePtr = nativePtr;
        public ImNodesContextPtr(IntPtr nativePtr) => NativePtr = (ImNodesContext*)nativePtr;
    }

    public unsafe struct ImNodesEditorContextPtr
    {
        public ImNodesEditorContext* NativePtr { get; }
        public ImNodesEditorContextPtr(ImNodesEditorContext* nativePtr) => NativePtr = nativePtr;
        public ImNodesEditorContextPtr(IntPtr nativePtr) => NativePtr = (ImNodesEditorContext*)nativePtr;
    }

    public unsafe struct ImNodesContext
    {
        ImNodesEditorContextPtr DefaultEditorCtx;
        ImNodesEditorContextPtr EditorCtx;

        // Canvas draw list and helper state
        ImDrawList* CanvasDrawList;
        ImGuiStorage NodeIdxToSubmissionIdx;
        ImVector<int> NodeIdxSubmissionOrder;
        ImVector<int> NodeIndicesOverlappingWithMouse;
        ImVector<int> OccludedPinIndices;

        // Canvas extents
        ImVec2 CanvasOriginScreenSpace;
        ImRect CanvasRectScreenSpace;

        // Debug helpers
        ImNodesScope CurrentScope;

        // Configuration state
        ImNodesIO Io;
        ImNodesStyle Style;
        ImVector<ImNodesColElement> ColorModifierStack;
        ImVector<ImNodesStyleVarElement> StyleModifierStack;
        ImGuiTextBuffer TextBuffer;

        int CurrentAttributeFlags;
        ImVector<int> AttributeFlagStack;

        // UI element state
        int CurrentNodeIdx;
        int CurrentPinIdx;
        int CurrentAttributeId;

        ImOptionalIndex HoveredNodeIdx;
        ImOptionalIndex HoveredLinkIdx;
        ImOptionalIndex HoveredPinIdx;

        ImOptionalIndex DeletedLinkIdx;
        ImOptionalIndex SnapLinkIdx;

        int ImNodesUIState;

        int ActiveAttributeId;
        bool ActiveAttribute;

        // ImGui::IO cache

        ImVec2 MousePos;

        bool LeftMouseClicked;
        bool LeftMouseReleased;
        bool AltMouseClicked;
        bool LeftMouseDragging;
        bool AltMouseDragging;
        float AltMouseScrollDelta;
        bool MultipleSelectModifier;
    }

    public unsafe struct ImNodesEditorContext
    {
        ImObjectPool<ImNodeData> Nodes;
        ImObjectPool<ImPinData> Pins;
        ImObjectPool<ImLinkData> Links;

        ImVector<int> NodeDepthOrder;

        // ui related fields
        ImVec2 Panning;
        ImVec2 AutoPanningDelta;
        // Minimum and maximum extents of all content in grid space. Valid after final
        // ImNodes::EndNode() call.
        ImRect GridContentBounds;

        ImVector<int> SelectedNodeIndices;
        ImVector<int> SelectedLinkIndices;

        // Relative origins of selected nodes for snapping of dragged nodes
        ImVector<ImVec2> SelectedNodeOffsets;
        // Offset of the primary node origin relative to the mouse cursor.
        ImVec2 PrimaryNodeOffset;

        ImClickInteractionState ClickInteraction;

        // Mini-map state set by MiniMap()

        bool MiniMapEnabled;
        ImNodesMiniMapLocation MiniMapLocation;
        float MiniMapSizeFraction;
        ImNodesMiniMapNodeHoveringCallback MiniMapNodeHoveringCallback;
        ImNodesMiniMapNodeHoveringCallbackUserData MiniMapNodeHoveringCallbackUserData;

        // Mini-map state set during EndNodeEditor() call

        ImRect MiniMapRectScreenSpace;
        ImRect MiniMapContentScreenSpace;
        float MiniMapScaling;
    }
}

